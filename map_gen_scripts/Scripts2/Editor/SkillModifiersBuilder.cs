using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class SkillModifiersBuilder : MonoBehaviour
{
    [MenuItem("Assets/Servece/BuildSkillModifiers")]
    public static void BuildSkillModifiers()
    {
        var racesGUIDs = AssetDatabase.FindAssets("t: Race");
        var classesGUIDs = AssetDatabase.FindAssets("t: PersonClass");
        var skillsGUISs = AssetDatabase.FindAssets("t: Skill");
        var rangeSelector = AssetDatabase.LoadAssetAtPath<FromRangeRandomSelector>(AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets("UniformRangeSelector")[0]));

        foreach (var r in racesGUIDs)
        {
            var race = AssetDatabase.LoadAssetAtPath<Race>(AssetDatabase.GUIDToAssetPath(r));
            foreach (var s in skillsGUISs)
            {
                var skill = AssetDatabase.LoadAssetAtPath<Skill>(AssetDatabase.GUIDToAssetPath(s));
                var sm = new SkillModifier();
                sm.name = race.RaceName + skill.SkillName;
                sm.Set(skill, 1, 3, rangeSelector);
                var path = Application.dataPath + "/Entities/Modifiers/Race/" + race.RaceName + "/" + skill.Category.CategoryName + "/";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                AssetDatabase.CreateAsset(sm, "Assets/Entities/Modifiers/Race/" + race.RaceName + "/" + skill.Category.CategoryName + "/" + sm.name + ".asset");
            }
        }
        foreach (var c in classesGUIDs)
        {
            var pClass = AssetDatabase.LoadAssetAtPath<PersonClass>(AssetDatabase.GUIDToAssetPath(c));
            foreach (var s in skillsGUISs)
            {
                var skill = AssetDatabase.LoadAssetAtPath<Skill>(AssetDatabase.GUIDToAssetPath(s));
                var sm = new SkillModifier();
                sm.name = pClass.ClassName + skill.SkillName;
                sm.Set(skill, 1, 3, rangeSelector);
                var path = Application.dataPath + "/Entities/Modifiers/Class/" + pClass.ClassName + "/" + skill.Category.CategoryName + "/";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                AssetDatabase.CreateAsset(sm, "Assets/Entities/Modifiers/Class/" + pClass.ClassName + "/" + skill.Category.CategoryName + "/" + sm.name + ".asset");
            }
        }
    }
}
