using System;
using System.Collections;
using System.Collections.Generic;
using GuildOfHeroes.Core;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class HeroTests
{
	[Test]
	public void Create_creationWithSomeName_creationOk()
	{
		Hero hero = Hero.Create("my hero");
		Assert.IsNotNull(hero);
	}
	[Test]
	public void Create_creationWithEmptyName_throwsException()
	{
		var exc = Assert.Throws<ArgumentException>(
			() => Hero.Create("")
		);
		Assert.IsTrue(
			exc.Message.ToLower().Contains("name can not be empty")
		);
	}

	[Test]
	public void Create_creationWithSomeName_skillsCountEqualsZero()
	{
		Hero hero = Hero.Create("my hero");
		Assert.AreEqual(0, hero.SkillsCount);
	}


	[Test]
	public void Name_creationWithSomeName_returnCorrectName()
	{
		Hero hero = Hero.Create("my hero");
		Assert.AreEqual("my hero", hero.Name);
	}

	[Test]
	public void AddSkill_addSomeSkill_skillsCountEqualsOne()
	{
		Hero hero = Hero.Create("my hero");
		hero.AddSkill("some skill", 1);
		Assert.AreEqual(1, hero.SkillsCount);
	}

	[Test]
	public void AddSkill_addSkillWithEmptyName_throwsException()
	{
		Hero hero = Hero.Create("my hero");
		var exc = Assert.Throws<ArgumentException>(
			() => hero.AddSkill("", 1)
		);
		Assert.IsTrue(
			exc.Message.ToLower().Contains("skill name can not be empty")
		);
	}

	[Test]
	public void AddSkill_addSkillWithNegativeValue_throwsException()
	{
		Hero hero = Hero.Create("my hero");
		var exc = Assert.Throws<ArgumentException>(
			() => hero.AddSkill("some skill", -1)
		);
		Assert.IsTrue(
			exc.Message.ToLower().Contains("skill value must be positive")
		);
	}

	[Test]
	public void AddSkill_addSkillWithSomeValue_getSkillReturnCorrectValue()
	{
		Hero hero = Hero.Create("my hero");
		hero.AddSkill("some skill", 1);
		Assert.AreEqual(1, hero.GetSkillValue("some skill"));
	}

	[Test]
	public void AddSkill_addSkillWithExistingName_rewriteOldValue()
	{
		Hero hero = Hero.Create("my hero");
		hero.AddSkill("some skill", 1);
		hero.AddSkill("some skill", 2);
		Assert.AreEqual(2, hero.GetSkillValue("some skill"));
	}

	[Test]
	public void GetSkill_getSkillWithUnexistingName_returnZero()
	{
		Hero hero = Hero.Create("my hero");
		Assert.AreEqual(0, hero.GetSkillValue("some skill"));
	}


	[Test]
	public void GetSkills_getSkillsForEmptyHero_returnEmptyMap()
	{
		Hero hero = Hero.Create("my hero");
		Assert.AreEqual(0, hero.Skills.Count);
	}

	[Test]
	public void GetSkills_addOneSkill_returnCorrectMap()
	{
		Hero hero = Hero.Create("my hero");
		hero.AddSkill("some skill", 10);
		var skills = hero.Skills;
		Assert.AreEqual(10, skills["some skill"]);
		Assert.AreEqual(1, skills.Count);
	}

	[Test]
	public void GetSkills_addTwoSkills_returnCorrectMap()
	{
		Hero hero = Hero.Create("my hero");
		hero.AddSkill("skill 1", 10);
		hero.AddSkill("skill 2", 20);
		var skills = hero.Skills;
		Assert.AreEqual(2, skills.Count);
		Assert.AreEqual(10, skills["skill 1"]);
		Assert.AreEqual(20, skills["skill 2"]);
	}


	[Test]
	public void GetDailyFee_createNewHero_dailyFeeEqualsZero()
	{
		Hero hero = Hero.Create("my hero");
		Assert.AreEqual(0, hero.DailyFee);
	}

	[Test]
	public void SetDailyFee_setPositiveFee_dailyFeeEqualsSettedValue()
	{
		Hero hero = Hero.Create("my hero");
		hero.DailyFee = 10;
		Assert.AreEqual(10, hero.DailyFee);
	}

	[Test]
	public void SetDailyFee_setNegativeFee_throwsException()
	{
		Hero hero = Hero.Create("my hero");
		var exc = Assert.Throws<ArgumentException>(
			() => hero.DailyFee = -10
		);
		Assert.IsTrue(
			exc.Message.ToLower().Contains("fee can not be negative")
		);
	}
}
