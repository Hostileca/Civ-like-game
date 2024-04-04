using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Resources.Scripts.Game.ForCivilization
{
	public class ScientificResearch
	{
		public string Name { get; private set; }

		public int ScienceCost { get; private set; }

		public List<string> NecessaryResearch;

		public string Description { get; private set; }

		public ScientificResearch(string name, int scienceCost,List<string> necessaryResearch, string description)
		{
			Name = name;
			ScienceCost = scienceCost;
			NecessaryResearch = necessaryResearch;
			Description = description;
		}
	}
}
