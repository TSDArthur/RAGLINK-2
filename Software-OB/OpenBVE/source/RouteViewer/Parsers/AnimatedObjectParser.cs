using System;
using System.Globalization;
using OpenBveApi;
using OpenBveApi.FunctionScripting;
using OpenBveApi.Interface;
using OpenBveApi.Math;
using OpenBveApi.Objects;
using OpenBveApi.Textures;

namespace OpenBve {
	internal static class AnimatedObjectParser {

		// parse animated object config
		/// <summary>Loads a collection of animated objects from a file.</summary>
		/// <param name="FileName">The text file to load the animated object from. Must be an absolute file name.</param>
		/// <param name="Encoding">The encoding the file is saved in. If the file uses a byte order mark, the encoding indicated by the byte order mark is used and the Encoding parameter is ignored.</param>
		/// <returns>The collection of animated objects.</returns>
		internal static ObjectManager.AnimatedObjectCollection ReadObject(string FileName, System.Text.Encoding Encoding) {
			System.Globalization.CultureInfo Culture = System.Globalization.CultureInfo.InvariantCulture;
			ObjectManager.AnimatedObjectCollection Result = new ObjectManager.AnimatedObjectCollection
			{
				Objects = new ObjectManager.AnimatedObject[4]
			};
			int ObjectCount = 0;
			// load file
			string[] Lines = System.IO.File.ReadAllLines(FileName, Encoding);
			bool rpnUsed = false;
			for (int i = 0; i < Lines.Length; i++) {
				int j = Lines[i].IndexOf(';');
				if (j >= 0) {
					Lines[i] = Lines[i].Substring(0, j).Trim();
				} else {
					Lines[i] = Lines[i].Trim();
				}
				if (Lines[i].IndexOf("functionrpn", StringComparison.OrdinalIgnoreCase) >= 0) {
					rpnUsed = true;
				}
			}
			if (rpnUsed) {
				Interface.AddMessage(MessageType.Error, false, "An animated object file contains RPN functions. These were never meant to be used directly, only for debugging. They won't be supported indefinately. Please get rid of them in file " + FileName);
			}
			for (int i = 0; i < Lines.Length; i++) {
				if (Lines[i].Length != 0) {
					switch (Lines[i].ToLowerInvariant()) {
						case "[include]":
							{
								i++;
								Vector3 position = Vector3.Zero;
								UnifiedObject[] obj = new UnifiedObject[4];
								int objCount = 0;
								while (i < Lines.Length && !(Lines[i].StartsWith("[", StringComparison.Ordinal) & Lines[i].EndsWith("]", StringComparison.Ordinal))) {
									if (Lines[i].Length != 0) {
										int j = Lines[i].IndexOf("=", StringComparison.Ordinal);
										if (j > 0) {
											string a = Lines[i].Substring(0, j).TrimEnd();
											string b = Lines[i].Substring(j + 1).TrimStart();
											switch (a.ToLowerInvariant()) {
												case "position":
													{
														string[] s = b.Split(',');
														if (s.Length == 3) {
															double x, y, z;
															if (!double.TryParse(s[0], System.Globalization.NumberStyles.Float, Culture, out x)) {
																Interface.AddMessage(MessageType.Error, false, "X is invalid in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
															} else if (!double.TryParse(s[1], System.Globalization.NumberStyles.Float, Culture, out y)) {
																Interface.AddMessage(MessageType.Error, false, "Y is invalid in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
															} else if (!double.TryParse(s[2], System.Globalization.NumberStyles.Float, Culture, out z)) {
																Interface.AddMessage(MessageType.Error, false, "Z is invalid in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
															} else {
																position = new Vector3(x, y, z);
															}
														} else {
															Interface.AddMessage(MessageType.Error, false, "Exactly 3 arguments are expected in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
														}
													} break;
												default:
													Interface.AddMessage(MessageType.Error, false, "The attribute " + a + " is not supported at line " + (i + 1).ToString(Culture) + " in file " + FileName);
													break;
											}
										} else {
											string Folder = System.IO.Path.GetDirectoryName(FileName);
											if (Path.ContainsInvalidChars(Lines[i])) {
												Interface.AddMessage(MessageType.Error, false, Lines[i] + " contains illegal characters at line " + (i + 1).ToString(Culture) + " in file " + FileName);
											} else {
												string file = OpenBveApi.Path.CombineFile(Folder, Lines[i]);
												if (System.IO.File.Exists(file)) {
													if (obj.Length == objCount) {
														Array.Resize<UnifiedObject>(ref obj, obj.Length << 1);
													}
													obj[objCount] = ObjectManager.LoadObject(file, Encoding, false);
													objCount++;
												} else {
													Interface.AddMessage(MessageType.Error, true, "File " + file + " not found at line " + (i + 1).ToString(Culture) + " in file " + FileName);
												}
											}
										}
									}
									i++;
								}
								i--;
								for (int j = 0; j < objCount; j++) {
									if (obj[j] != null) {
										if (obj[j] is ObjectManager.StaticObject) {
											ObjectManager.StaticObject s = (ObjectManager.StaticObject)obj[j];
											s.Dynamic = true;
											if (ObjectCount >= Result.Objects.Length) {
												Array.Resize<ObjectManager.AnimatedObject>(ref Result.Objects, Result.Objects.Length << 1);
											}
											ObjectManager.AnimatedObject a = new ObjectManager.AnimatedObject();
											ObjectManager.AnimatedObjectState aos = new ObjectManager.AnimatedObjectState(s, position);
											a.States = new ObjectManager.AnimatedObjectState[] { aos };
											Result.Objects[ObjectCount] = a;
											ObjectCount++;
										} else if (obj[j] is ObjectManager.AnimatedObjectCollection) {
											ObjectManager.AnimatedObjectCollection a = (ObjectManager.AnimatedObjectCollection)obj[j];
											for (int k = 0; k < a.Objects.Length; k++) {
												if (ObjectCount >= Result.Objects.Length) {
													Array.Resize<ObjectManager.AnimatedObject>(ref Result.Objects, Result.Objects.Length << 1);
												}
												for (int h = 0; h < a.Objects[k].States.Length; h++) {
													a.Objects[k].States[h].Position.X += position.X;
													a.Objects[k].States[h].Position.Y += position.Y;
													a.Objects[k].States[h].Position.Z += position.Z;
												}
												Result.Objects[ObjectCount] = a.Objects[k];
												ObjectCount++;
											}
										}
									}
								}
							}
							break;
						case "[object]":
							{
								i++;
								if (Result.Objects.Length == ObjectCount) {
									Array.Resize<ObjectManager.AnimatedObject>(ref Result.Objects, Result.Objects.Length << 1);
								}
								Result.Objects[ObjectCount] = new ObjectManager.AnimatedObject();
								Result.Objects[ObjectCount].States = new ObjectManager.AnimatedObjectState[] { };
								Result.Objects[ObjectCount].CurrentState = -1;
								Result.Objects[ObjectCount].TranslateXDirection = Vector3.Right;
								Result.Objects[ObjectCount].TranslateYDirection = Vector3.Down;
								Result.Objects[ObjectCount].TranslateZDirection = Vector3.Forward;
								Result.Objects[ObjectCount].RotateXDirection = Vector3.Right;
								Result.Objects[ObjectCount].RotateYDirection = Vector3.Down;
								Result.Objects[ObjectCount].RotateZDirection = Vector3.Forward;
								Result.Objects[ObjectCount].TextureShiftXDirection = new Vector2(1.0, 0.0);
								Result.Objects[ObjectCount].TextureShiftYDirection = new Vector2(0.0, 1.0);
								Result.Objects[ObjectCount].RefreshRate = 0.0;
								Result.Objects[ObjectCount].ObjectIndex = -1;
								Vector3 Position = Vector3.Zero;
								double RotateX = 0;
								bool StaticXRotation = false;
								double RotateY = 0;
								bool StaticYRotation = false;
								double RotateZ = 0;
								bool StaticZRotation = false;
								bool timetableUsed = false;
								string[] StateFiles = null;
								string StateFunctionRpn = null;
								bool StateFunctionIsPostfix = false;
								int StateFunctionLine = -1;
								while (i < Lines.Length && !(Lines[i].StartsWith("[", StringComparison.Ordinal) & Lines[i].EndsWith("]", StringComparison.Ordinal))) {
									if (Lines[i].Length != 0) {
										int j = Lines[i].IndexOf("=", StringComparison.Ordinal);
										if (j > 0) {
											string a = Lines[i].Substring(0, j).TrimEnd();
											string b = Lines[i].Substring(j + 1).TrimStart();
											switch (a.ToLowerInvariant()) {
												case "position":
													{
														string[] s = b.Split(',');
														if (s.Length == 3) {
															double x, y, z;
															if (!double.TryParse(s[0], System.Globalization.NumberStyles.Float, Culture, out x)) {
																Interface.AddMessage(MessageType.Error, false, "X is invalid in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
															} else if (!double.TryParse(s[1], System.Globalization.NumberStyles.Float, Culture, out y)) {
																Interface.AddMessage(MessageType.Error, false, "Y is invalid in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
															} else if (!double.TryParse(s[2], System.Globalization.NumberStyles.Float, Culture, out z)) {
																Interface.AddMessage(MessageType.Error, false, "Z is invalid in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
															} else {
																Position = new Vector3(x, y, z);
															}
														} else {
															Interface.AddMessage(MessageType.Error, false, "Exactly 3 arguments are expected in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
														}
													} break;
												case "states":
													{
														string[] s = b.Split(',');
														if (s.Length >= 1) {
															string Folder = System.IO.Path.GetDirectoryName(FileName);
															StateFiles = new string[s.Length];
														    bool NullObject = true;
															for (int k = 0; k < s.Length; k++) {
																s[k] = s[k].Trim();
																if (s[k].Length == 0) {
																	Interface.AddMessage(MessageType.Error, false, "File" + k.ToString(Culture) + " is an empty string - did you mean something else? - in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
																	StateFiles[k] = null;
																} else if (Path.ContainsInvalidChars(s[k])) {
																	Interface.AddMessage(MessageType.Error, false, "File" + k.ToString(Culture) + " contains illegal characters in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
																	StateFiles[k] = null;
																} else {
																	StateFiles[k] = OpenBveApi.Path.CombineFile(Folder, s[k]);
																	if (!System.IO.File.Exists(StateFiles[k])) {
																		Interface.AddMessage(MessageType.Error, true, "File " + StateFiles[k] + " not found in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
																		StateFiles[k] = null;
																	}
																}
															    if (StateFiles[k] != null)
															    {
															        NullObject = false;
															    }
															}
															if (NullObject == true)
															{
																Interface.AddMessage(MessageType.Error, false, "No statefiles were found in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
																return null;
															}
														} else {
															Interface.AddMessage(MessageType.Error, false, "At least one argument is expected in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
															return null;
														}
													} break;
												case "statefunction":
													try
													{
														StateFunctionLine = i;
														StateFunctionRpn = b;
													}
													catch (Exception ex)
													{
														Interface.AddMessage(MessageType.Error, false, ex.Message + " in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
													} break;
												case "statefunctionrpn":
													{
														StateFunctionLine = i;
														StateFunctionRpn = b;
														StateFunctionIsPostfix = true;
													}
													break;
												case "translatexdirection":
												case "translateydirection":
												case "translatezdirection":
													{
														string[] s = b.Split(',');
														if (s.Length == 3) {
															double x, y, z;
															if (!double.TryParse(s[0], System.Globalization.NumberStyles.Float, Culture, out x)) {
																Interface.AddMessage(MessageType.Error, false, "X is invalid in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
															} else if (!double.TryParse(s[1], System.Globalization.NumberStyles.Float, Culture, out y)) {
																Interface.AddMessage(MessageType.Error, false, "Y is invalid in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
															} else if (!double.TryParse(s[2], System.Globalization.NumberStyles.Float, Culture, out z)) {
																Interface.AddMessage(MessageType.Error, false, "Z is invalid in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
															} else {
																switch (a.ToLowerInvariant()) {
																	case "translatexdirection":
																		Result.Objects[ObjectCount].TranslateXDirection = new Vector3(x, y, z);
																		break;
																	case "translateydirection":
																		Result.Objects[ObjectCount].TranslateYDirection = new Vector3(x, y, z);
																		break;
																	case "translatezdirection":
																		Result.Objects[ObjectCount].TranslateZDirection = new Vector3(x, y, z);
																		break;
																}
															}
														} else {
															Interface.AddMessage(MessageType.Error, false, "Exactly 3 arguments are expected in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
														}
													} break;
												case "translatexfunction":
													try {
														Result.Objects[ObjectCount].TranslateXFunction = new FunctionScript(Program.CurrentHost, b, true);
													} catch (Exception ex) {
														Interface.AddMessage(MessageType.Error, false, ex.Message + " in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
													} break;
												case "translateyfunction":
													try {
														Result.Objects[ObjectCount].TranslateYFunction = new FunctionScript(Program.CurrentHost, b, true);
													} catch (Exception ex) {
														Interface.AddMessage(MessageType.Error, false, ex.Message + " in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
													} break;
												case "translatezfunction":
													try {
														Result.Objects[ObjectCount].TranslateZFunction = new FunctionScript(Program.CurrentHost, b, true);
													} catch (Exception ex) {
														Interface.AddMessage(MessageType.Error, false, ex.Message + " in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
													} break;
												case "translatexfunctionrpn":
													try {
														Result.Objects[ObjectCount].TranslateXFunction = new FunctionScript(Program.CurrentHost, b, false);
													} catch (Exception ex) {
														Interface.AddMessage(MessageType.Error, false, ex.Message + " in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
													} break;
												case "translateyfunctionrpn":
													try {
														Result.Objects[ObjectCount].TranslateYFunction = new FunctionScript(Program.CurrentHost, b, false);
													} catch (Exception ex) {
														Interface.AddMessage(MessageType.Error, false, ex.Message + " in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
													} break;
												case "translatezfunctionrpn":
													try {
														Result.Objects[ObjectCount].TranslateZFunction = new FunctionScript(Program.CurrentHost, b, false);
													} catch (Exception ex) {
														Interface.AddMessage(MessageType.Error, false, ex.Message + " in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
													} break;
												case "rotatexdirection":
												case "rotateydirection":
												case "rotatezdirection":
													{
														string[] s = b.Split(',');
														if (s.Length == 3) {
															double x, y, z;
															if (!double.TryParse(s[0], System.Globalization.NumberStyles.Float, Culture, out x)) {
																Interface.AddMessage(MessageType.Error, false, "X is invalid in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
															} else if (!double.TryParse(s[1], System.Globalization.NumberStyles.Float, Culture, out y)) {
																Interface.AddMessage(MessageType.Error, false, "Y is invalid in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
															} else if (!double.TryParse(s[2], System.Globalization.NumberStyles.Float, Culture, out z)) {
																Interface.AddMessage(MessageType.Error, false, "Z is invalid in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
															} else if (x == 0.0 & y == 0.0 & z == 0.0) {
																Interface.AddMessage(MessageType.Error, false, "The direction indicated by X, Y and Z is expected to be non-zero in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
															} else {
																switch (a.ToLowerInvariant()) {
																	case "rotatexdirection":
																		Result.Objects[ObjectCount].RotateXDirection = new Vector3(x, y, z);
																		break;
																	case "rotateydirection":
																		Result.Objects[ObjectCount].RotateYDirection = new Vector3(x, y, z);
																		break;
																	case "rotatezdirection":
																		Result.Objects[ObjectCount].RotateZDirection = new Vector3(x, y, z);
																		break;
																}
															}
														} else {
															Interface.AddMessage(MessageType.Error, false, "Exactly 3 arguments are expected in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
														}
													} break;
												case "rotatexfunction":
													try
													{
														if (double.TryParse(b, NumberStyles.Float, Culture, out RotateX))
														{
															//A function script must be evaluated every frame, no matter if it is a constant value
															//If we add this to the position instead, this gives a minor speedup
															StaticXRotation = true;
														}
														Result.Objects[ObjectCount].RotateXFunction = new FunctionScript(Program.CurrentHost, b, true);
													}
													catch (Exception ex)
													{
														Interface.AddMessage(MessageType.Error, false, ex.Message + " in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
													} break;
												case "rotateyfunction":
													try
													{
														if (double.TryParse(b, NumberStyles.Float, Culture, out RotateY))
														{
															StaticYRotation = true;
														}
														Result.Objects[ObjectCount].RotateYFunction = new FunctionScript(Program.CurrentHost, b, true);
													}
													catch (Exception ex)
													{
														Interface.AddMessage(MessageType.Error, false, ex.Message + " in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
													} break;
												case "rotatezfunction":
													try
													{
														if (double.TryParse(b, NumberStyles.Float, Culture, out RotateZ))
														{
															StaticZRotation = true;
														}
														Result.Objects[ObjectCount].RotateZFunction = new FunctionScript(Program.CurrentHost, b, true);
													}
													catch (Exception ex)
													{
														Interface.AddMessage(MessageType.Error, false, ex.Message + " in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
													} break;
												case "rotatexfunctionrpn":
													try
													{
														Result.Objects[ObjectCount].RotateXFunction = new FunctionScript(Program.CurrentHost, b, false);
													}
													catch (Exception ex)
													{
														Interface.AddMessage(MessageType.Error, false, ex.Message + " in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
													} break;
												case "rotateyfunctionrpn":
													try
													{
														Result.Objects[ObjectCount].RotateYFunction = new FunctionScript(Program.CurrentHost, b, false);
													}
													catch (Exception ex)
													{
														Interface.AddMessage(MessageType.Error, false, ex.Message + " in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
													} break;
												case "rotatezfunctionrpn":
													try
													{
														Result.Objects[ObjectCount].RotateZFunction = new FunctionScript(Program.CurrentHost, b, false);
													}
													catch (Exception ex)
													{
														Interface.AddMessage(MessageType.Error, false, ex.Message + " in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
													} break;
												case "rotatexdamping":
												case "rotateydamping":
												case "rotatezdamping":
													{
														string[] s = b.Split(',');
														if (s.Length == 2) {
															double nf, dr;
															if (!double.TryParse(s[0], System.Globalization.NumberStyles.Float, Culture, out nf)) {
																Interface.AddMessage(MessageType.Error, false, "NaturalFrequency is invalid in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
															} else if (!double.TryParse(s[1], System.Globalization.NumberStyles.Float, Culture, out dr)) {
																Interface.AddMessage(MessageType.Error, false, "DampingRatio is invalid in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
															} else if (nf <= 0.0) {
																Interface.AddMessage(MessageType.Error, false, "NaturalFrequency is expected to be positive in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
															} else if (dr <= 0.0) {
																Interface.AddMessage(MessageType.Error, false, "DampingRatio is expected to be positive in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
															} else {
																switch (a.ToLowerInvariant()) {
																	case "rotatexdamping":
																		Result.Objects[ObjectCount].RotateXDamping = new Damping(nf, dr);
																		break;
																	case "rotateydamping":
																		Result.Objects[ObjectCount].RotateYDamping = new Damping(nf, dr);
																		break;
																	case "rotatezdamping":
																		Result.Objects[ObjectCount].RotateZDamping = new Damping(nf, dr);
																		break;
																}
															}
														} else {
															Interface.AddMessage(MessageType.Error, false, "Exactly 2 arguments are expected in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
														}
													} break;
												case "textureshiftxdirection":
												case "textureshiftydirection":
													{
														string[] s = b.Split(',');
														if (s.Length == 2) {
															double x, y;
															if (!double.TryParse(s[0], System.Globalization.NumberStyles.Float, Culture, out x)) {
																Interface.AddMessage(MessageType.Error, false, "X is invalid in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
															} else if (!double.TryParse(s[1], System.Globalization.NumberStyles.Float, Culture, out y)) {
																Interface.AddMessage(MessageType.Error, false, "Y is invalid in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
															} else {
																switch (a.ToLowerInvariant()) {
																	case "textureshiftxdirection":
																		Result.Objects[ObjectCount].TextureShiftXDirection = new Vector2(x, y);
																		break;
																	case "textureshiftydirection":
																		Result.Objects[ObjectCount].TextureShiftYDirection = new Vector2(x, y);
																		break;
																}
															}
														} else {
															Interface.AddMessage(MessageType.Error, false, "Exactly 2 arguments are expected in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
														}
													} break;
												case "textureshiftxfunction":
													try {
														Result.Objects[ObjectCount].TextureShiftXFunction = new FunctionScript(Program.CurrentHost, b, true);
													} catch (Exception ex) {
														Interface.AddMessage(MessageType.Error, false, ex.Message + " in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
													} break;
												case "textureshiftyfunction":
													try {
														Result.Objects[ObjectCount].TextureShiftYFunction = new FunctionScript(Program.CurrentHost, b, true);
													} catch (Exception ex) {
														Interface.AddMessage(MessageType.Error, false, ex.Message + " in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
													} break;
												case "textureshiftxfunctionrpn":
													try {
														Result.Objects[ObjectCount].TextureShiftXFunction = new FunctionScript(Program.CurrentHost, b, false);
													} catch (Exception ex) {
														Interface.AddMessage(MessageType.Error, false, ex.Message + " in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
													} break;
												case "textureshiftyfunctionrpn":
													try {
														Result.Objects[ObjectCount].TextureShiftYFunction = new FunctionScript(Program.CurrentHost, b, false);
													} catch (Exception ex) {
														Interface.AddMessage(MessageType.Error, false, ex.Message + " in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
													} break;
												case "textureoverride":
													switch (b.ToLowerInvariant()) {
														case "none":
															break;
														case "timetable":
															if (!timetableUsed) {
																Timetable.AddObjectForCustomTimetable(Result.Objects[ObjectCount]);
																timetableUsed = true;
															}
															break;
														default:
															Interface.AddMessage(MessageType.Error, false, "Unrecognized value in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
															break;
													}
													break;
												case "refreshrate":
													{
														double r;
														if (!double.TryParse(b, System.Globalization.NumberStyles.Float, Culture, out r)) {
															Interface.AddMessage(MessageType.Error, false, "Value is invalid in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
														} else if (r < 0.0) {
															Interface.AddMessage(MessageType.Error, false, "Value is expected to be non-negative in " + a + " at line " + (i + 1).ToString(Culture) + " in file " + FileName);
														} else {
															Result.Objects[ObjectCount].RefreshRate = r;
														}
													} break;
												default:
													Interface.AddMessage(MessageType.Error, false, "The attribute " + a + " is not supported at line " + (i + 1).ToString(Culture) + " in file " + FileName);
													break;
											}
										} else {
											Interface.AddMessage(MessageType.Error, false, "Invalid statement " + Lines[i] + " encountered at line " + (i + 1).ToString(Culture) + " in file " + FileName);
											return null;
										}
									}
									i++;
								}
								i--;
								if (StateFiles != null)
								{
									// create the object
									if (timetableUsed)
									{
										if (StateFunctionRpn != null)
										{
											StateFunctionRpn = "timetable 0 == " + StateFunctionRpn + " -1 ?";
										}
										else
										{
											StateFunctionRpn = "timetable";
										}
									}
									if (StateFunctionRpn != null)
									{
										try
										{
											Result.Objects[ObjectCount].StateFunction = new FunctionScript(Program.CurrentHost, StateFunctionRpn, !StateFunctionIsPostfix);
										}
										catch (Exception ex)
										{
											Interface.AddMessage(MessageType.Error, false, ex.Message + " in StateFunction at line " + (StateFunctionLine + 1).ToString(Culture) + " in file " + FileName);
										}
									}
									Result.Objects[ObjectCount].States = new ObjectManager.AnimatedObjectState[StateFiles.Length];
									bool ForceTextureRepeatX = Result.Objects[ObjectCount].TextureShiftXFunction != null & Result.Objects[ObjectCount].TextureShiftXDirection.X != 0.0 |
									                           Result.Objects[ObjectCount].TextureShiftYFunction != null & Result.Objects[ObjectCount].TextureShiftYDirection.X != 0.0;
									bool ForceTextureRepeatY = Result.Objects[ObjectCount].TextureShiftXFunction != null & Result.Objects[ObjectCount].TextureShiftXDirection.Y != 0.0 |
									                           Result.Objects[ObjectCount].TextureShiftYFunction != null & Result.Objects[ObjectCount].TextureShiftYDirection.Y != 0.0;
									for (int k = 0; k < StateFiles.Length; k++)
									{
										Result.Objects[ObjectCount].States[k].Position = Vector3.Zero;
										if (StateFiles[k] != null)
										{
											Result.Objects[ObjectCount].States[k].Object = ObjectManager.LoadStaticObject(StateFiles[k], Encoding, false);
											if (Result.Objects[ObjectCount].States[k].Object != null)
											{
												Result.Objects[ObjectCount].States[k].Object.Dynamic = true;
												for (int l = 0; l < Result.Objects[ObjectCount].States[k].Object.Mesh.Materials.Length; l++)
												{
													if (ForceTextureRepeatX && ForceTextureRepeatY)
													{
														Result.Objects[ObjectCount].States[k].Object.Mesh.Materials[l].WrapMode = OpenGlTextureWrapMode.RepeatRepeat;
													}
													else if (ForceTextureRepeatX)
													{
														
														switch (Result.Objects[ObjectCount].States[k].Object.Mesh.Materials[l].WrapMode)
														{
															case OpenGlTextureWrapMode.ClampRepeat:
																Result.Objects[ObjectCount].States[k].Object.Mesh.Materials[l].WrapMode = OpenGlTextureWrapMode.RepeatRepeat;
																break;
															case OpenGlTextureWrapMode.ClampClamp:
																Result.Objects[ObjectCount].States[k].Object.Mesh.Materials[l].WrapMode = OpenGlTextureWrapMode.RepeatClamp;
																break;
														}
													}
													else if (ForceTextureRepeatY)
													{
														
														switch (Result.Objects[ObjectCount].States[k].Object.Mesh.Materials[l].WrapMode)
														{
															case OpenGlTextureWrapMode.RepeatClamp:
																Result.Objects[ObjectCount].States[k].Object.Mesh.Materials[l].WrapMode = OpenGlTextureWrapMode.RepeatRepeat;
																break;
															case OpenGlTextureWrapMode.ClampClamp:
																Result.Objects[ObjectCount].States[k].Object.Mesh.Materials[l].WrapMode = OpenGlTextureWrapMode.ClampRepeat;
																break;
														}
													}
												}
											}
											
										}
										else
										{
											Result.Objects[ObjectCount].States[k].Object = null;
										}
										
									}
									for (int j = 0; j < Result.Objects[ObjectCount].States.Length; j++)
									{

										//Rotate X
										if (Result.Objects[ObjectCount].States[j].Object == null)
										{
											continue;
										}
										//Apply position
										Result.Objects[ObjectCount].States[j].Position = Position;
										//Test whether the object contains non static rotation functions
										//If so, the results may be off so don't optimise
										if (!StaticXRotation)
										{
											if (Result.Objects[ObjectCount].RotateXFunction != null)
											{
												continue;
											}
										}
										if (!StaticYRotation)
										{
											if (Result.Objects[ObjectCount].RotateYFunction != null)
											{
												continue;
											}
										}
										if (!StaticZRotation)
										{
											if (Result.Objects[ObjectCount].RotateZFunction != null)
											{
												continue;
											}
										}
										if (StaticXRotation)
										{
											ApplyStaticRotation(ref Result.Objects[ObjectCount].States[j].Object.Mesh, Result.Objects[ObjectCount].RotateXDirection, RotateX);
											Result.Objects[ObjectCount].RotateXFunction = null;
										}
										if (StaticYRotation)
										{
											ApplyStaticRotation(ref Result.Objects[ObjectCount].States[j].Object.Mesh, Result.Objects[ObjectCount].RotateYDirection, RotateY);
											Result.Objects[ObjectCount].RotateYFunction = null;
										}
										if (StaticZRotation)
										{
											ApplyStaticRotation(ref Result.Objects[ObjectCount].States[j].Object.Mesh, Result.Objects[ObjectCount].RotateZDirection, RotateZ);
											Result.Objects[ObjectCount].RotateZFunction = null;
										}
										
									}
								}
								else
								{
									Result.Objects[ObjectCount].States = new ObjectManager.AnimatedObjectState[] { };
								}
								ObjectCount++;
							}
							break;
						case "[sound]":
						case "[statechangesound]":
							//Only show the sound nag once per route, otherwise this could cause spam...
							if (!Program.SoundError)
							{
								Interface.AddMessage(MessageType.Information, false, "Animated objects containing sounds are only supported in openBVE v1.5.2.4+");
								Interface.AddMessage(MessageType.Information, false, "Object Viewer does not support sounds. Please use the main game to test these!");
								Program.SoundError = true;
							}
							i++;
							while (i < Lines.Length && !(Lines[i].StartsWith("[", StringComparison.Ordinal) & Lines[i].EndsWith("]", StringComparison.Ordinal)))
							{
								i++;
							}
							break;
						default:
							Interface.AddMessage(MessageType.Error, false, "Invalid statement " + Lines[i] + " encountered at line " + (i + 1).ToString(Culture) + " in file " + FileName);
							return null;
					}
				}
			}
			Array.Resize<ObjectManager.AnimatedObject>(ref Result.Objects, ObjectCount);
			return Result;
		}

		private static void ApplyStaticRotation(ref Mesh Mesh, Vector3 RotationDirection, double Angle)
		{
			//Update co-ords
			for (int i = 0; i < Mesh.Vertices.Length; i++)
			{
				Mesh.Vertices[i].Coordinates.Rotate(RotationDirection, Math.Cos(Angle), Math.Sin(Angle));
			}
			//Update normals
			for (int i = 0; i < Mesh.Faces.Length; i++)
			{
				for(int j = 0; j < Mesh.Faces[i].Vertices.Length; j++)
					if (!Vector3.IsZero(Mesh.Faces[i].Vertices[j].Normal))
					{
						Mesh.Faces[i].Vertices[j].Normal.Rotate(RotationDirection, Math.Cos(Angle), Math.Sin(Angle));
					}
			}
		}

	}
}
