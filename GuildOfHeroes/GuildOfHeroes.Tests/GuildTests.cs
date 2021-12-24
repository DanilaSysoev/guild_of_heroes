using System;
using System.Collections.Generic;
using System.Linq;
using GuildOfHeroes.Core;
using NUnit.Framework;

namespace GuildOfHeroes.Tests
{
    class GuildTests
    {
        [Test]
        public void Create_creationWithSomeName_creationOk()
        {
            Guild guild = Guild.Create("my guild");
            Assert.IsNotNull(guild);            
        }
		[Test]
		public void Create_creationWithEmptyName_throwsException()
		{
			var exc = Assert.Throws<ArgumentException>(
				() => Guild.Create("")
			);
			Assert.IsTrue(
				exc.Message.ToLower().Contains("name can not be empty")
			);
		}
		[Test]
        public void Create_creationWithSomeName_heroesCountZero()
		{
			Guild guild = Guild.Create("my guild");
			Assert.AreEqual(0, guild.HeroesCount);
		}
		[Test]
        public void Create_creationWithSomeName_heroesListIsEmpty()
		{
			Guild guild = Guild.Create("my guild");
			var heroes = guild.Heroes;
			Assert.AreEqual(0, heroes.Count);
		}

		[Test]
        public void GetName_creationWithSomeName_returnCorrectName()
		{
			Guild guild = Guild.Create("my guild");
			Assert.AreEqual("my guild", guild.Name);
		}

		[Test]
        public void AddHero_addFirstHero_heroesCountOne()
		{
			Guild guild = Guild.Create("my guild");
			Hero hero = Hero.Create("my hero");
			guild.AddHero(hero);
			Assert.AreEqual(1, guild.HeroesCount);
		}
		[Test]
        public void AddHero_addSecondHero_heroesCountIncrement()
		{
			Guild guild = Guild.Create("my guild");
			Hero hero1 = Hero.Create("my hero 1");
			Hero hero2 = Hero.Create("my hero 2");
			guild.AddHero(hero1);
			Assert.AreEqual(1, guild.HeroesCount);
			guild.AddHero(hero2);
			Assert.AreEqual(2, guild.HeroesCount);
		}
		[Test]
        public void AddHero_addNullHero_throwsException()
		{
			Guild guild = Guild.Create("my guild");
			var exc = Assert.Throws<ArgumentException>(
				() => guild.AddHero(null)
			);
			Assert.IsTrue(
				exc.Message.ToLower().Contains("hero can not be null")
			);
		}
		[Test]
        public void AddHero_addExistHero_throwsException()
		{
			Guild guild = Guild.Create("my guild");
			Hero hero1 = Hero.Create("my hero 1");
			guild.AddHero(hero1);
			var exc = Assert.Throws<ArgumentException>(
				() => guild.AddHero(hero1)
			);
			Assert.IsTrue(
				exc.Message.ToLower().Contains("hero already added")
			);
		}

		[Test]
		public void GetHeroes_addTwoHeroes_resultContainsBoth()
		{
			Guild guild = Guild.Create("my guild");
			Hero hero1 = Hero.Create("my hero 1");
			Hero hero2 = Hero.Create("my hero 2");
			guild.AddHero(hero1);
			guild.AddHero(hero2);

			var heroes = guild.Heroes;
			Assert.IsTrue(heroes.Contains(hero1));
			Assert.IsTrue(heroes.Contains(hero2));
			Assert.AreEqual(2, heroes.Count);
		}

		[Test]
        public void RemoveHero_removeExistHero_heroesCountDecrement()
		{
			Guild guild = Guild.Create("my guild");
			Hero hero1 = Hero.Create("my hero 1");
			Hero hero2 = Hero.Create("my hero 2");
			guild.AddHero(hero1);
			guild.AddHero(hero2);
			Assert.AreEqual(2, guild.HeroesCount);
			guild. RemoveHero(hero1);
			Assert.AreEqual(1, guild.HeroesCount);
		}
		[Test]
        public void RemoveHero_removeNonExistentHero_throwsException()
		{
			Guild guild = Guild.Create("my guild");
			Hero hero1 = Hero.Create("my hero 1");
			Hero hero2 = Hero.Create("my hero 2");
			guild.AddHero(hero1);
			var exc = Assert.Throws<InvalidOperationException>(
				() => guild.RemoveHero(hero2)
			);
			Assert.IsTrue(
				exc.Message.ToLower().Contains(
					"attempt to delete non-existent hero"
				)
			);
		}
		[Test]
        public void RemoveHero_removeExistHero_getHeroesReturnOk()
		{
			Guild guild = Guild.Create("my guild");
			Hero hero1 = Hero.Create("my hero 1");
			Hero hero2 = Hero.Create("my hero 2");
			guild.AddHero(hero1);
			guild.AddHero(hero2);
			guild.RemoveHero(hero1);

			var heroes = guild.Heroes;
			Assert.IsFalse(heroes.Contains(hero1));
            Assert.IsTrue(heroes.Contains(hero2));
			Assert.AreEqual(1, heroes.Count);
        }

        [Test]
        public void GetHeroesDailyPayment_creatNewGuild_equalsZero()
		{
			Guild guild = Guild.Create("my guild");
			Assert.AreEqual(0, guild.HeroesDailyPayment);
		}
		[Test]
        public void GetHeroesDailyPayment_twoHeroes_returnCorrect()
		{
			Guild guild = Guild.Create("my guild");
			Hero hero1 = Hero.Create("my hero 1");
			Hero hero2 = Hero.Create("my hero 2");
			hero1.DailyFee = 10;
			hero2.DailyFee = 20;
			guild.AddHero(hero1);
			guild.AddHero(hero2);

			Assert.AreEqual(30, guild.HeroesDailyPayment);
		}
	}
}
