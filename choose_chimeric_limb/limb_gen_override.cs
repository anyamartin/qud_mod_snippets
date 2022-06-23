using XRL.World;
using XRL.UI;
using HarmonyLib;
using System;
using System.Collections.Generic;

[HarmonyPatch(typeof(Anatomies))]
[HarmonyPatch("GetRandomBodyPartType")]
class AlteredAnatomies
{
    static bool Prefix(ref BodyPartType __result,bool IncludeVariants = true, bool? RequireAppendage = true, bool? RequireAbstract = false, bool RequireLiveCategory = true, int[] IncludeCategories = null, int[] ExcludeCategories = null, bool UseChimeraWeight = false){
        Dictionary<BodyPartType,List<BodyPartType>> partChildren =  new Dictionary<BodyPartType, List<BodyPartType>>();
		Dictionary<BodyPartType, int> dictionary = new Dictionary<BodyPartType, int>();
		int i = 0;
		for (int count = Anatomies.BodyPartTypeList.Count; i < count; i++)
		{
			BodyPartType bodyPartType = Anatomies.BodyPartTypeList[i];
			if ((IncludeVariants || !(bodyPartType.FinalType != bodyPartType.Type)) && (!RequireAppendage.HasValue || bodyPartType.Appendage.GetValueOrDefault() == RequireAppendage) && (!RequireAbstract.HasValue || bodyPartType.Abstract.GetValueOrDefault() == RequireAbstract) && (!RequireLiveCategory || BodyPartCategory.IsLiveCategory(bodyPartType.Category ?? 1)) && (IncludeCategories == null || Array.IndexOf(IncludeCategories, bodyPartType.Category ?? 1) != -1) && (ExcludeCategories == null || Array.IndexOf(ExcludeCategories, bodyPartType.Category ?? 1) != -1))
			{
				int num = ((!UseChimeraWeight || !bodyPartType.ChimeraWeight.HasValue) ? 1 : (bodyPartType.ChimeraWeight ?? 1));
				if (num > 0)
				{
					dictionary.Add(bodyPartType, num);
				}
			}
		}
        Dictionary<BodyPartType, int> dictionary2 = new Dictionary<BodyPartType, int>();
			{
				foreach (BodyPartType key in dictionary.Keys)
				{
					if (!(key.FinalType == key.Type))
					{
						continue;
					}
                    dictionary2.Add(key,dictionary[key]);
                    partChildren.Add(key,new List<BodyPartType>());
                    partChildren[key].Add(key);
					foreach (KeyValuePair<BodyPartType, int> item in dictionary)
					{
						if (item.Key != key && item.Key.FinalType == key.Type)
						{
                            partChildren[key].Add(item.Key);
							dictionary2[key] += item.Value;
						}
					}
				}
			}
        BodyPartType typeToChoose = dictionary2.GetRandomElement();

        char ch = 'a';
		List<string> ChoiceList = new List<string>();
		List<char> HotkeyList = new List<char>();
        foreach(BodyPartType bpt in partChildren[typeToChoose])
		{
			HotkeyList.Add(((ch <= 'z') ? ch++ : ' '));
			ChoiceList.Add(bpt.Name);
		}
        int selectedidx = Popup.ShowOptionList("Chimeric Growth", ChoiceList.ToArray(), HotkeyList.ToArray(), Intro: "You begin to grow a " +typeToChoose.Name + " equivalent. Choose the final shape of this organ.", AllowEscape: false);
        __result = partChildren[typeToChoose][selectedidx];
        return false;
    }
}
