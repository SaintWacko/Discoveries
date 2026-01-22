using System.Collections.Generic;
using Verse;
namespace Discoveries
{
    public class UnlockResearchOnDiscovery : DefModExtension
    {
        public ResearchProjectDef researchProject;
        public List<ResearchProjectDef> researchProjects;
        public IEnumerable<ResearchProjectDef> GetProjects()
        {
            if (researchProject != null) yield return researchProject;
            if (researchProjects != null)
            {
                foreach (var proj in researchProjects) yield return proj;
            }
        }
    }
}
