using System.Collections.Generic;

namespace RAGLINKCommons.RAGLINKPlatform
{
	public class UserInterfaceSwap
	{
		public enum formSummaryShowMode
		{
			STARTER = 0,
			SHOWER = 1
		};
		static public bool simulatorLaunchTrig = false;
		static public string projectGUID = string.Empty;
		static public string projectFilePath = string.Empty;
		static public formSummaryShowMode showMode = formSummaryShowMode.STARTER;
		static public List<ProjectsManager.ProjectErrorProvider> errorContent = new List<ProjectsManager.ProjectErrorProvider>();
	}
}
