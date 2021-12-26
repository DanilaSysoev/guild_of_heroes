using GuildOfHeroes.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuildOfHeroes.Tests
{
    class SettlementTests
	{
		[Test]
		public void Create_creationWithSomeName_creationOk()
		{
			Settlement settlement = Settlement.Create("my settlement");
			Assert.IsNotNull(settlement);
		}

		[Test]
		public void Create_creationWithEmptyName_throwsException()
		{
			var exc = Assert.Throws<ArgumentException>(
				() => Settlement.Create("")
			);
			Assert.IsTrue(
				exc.Message.ToLower().Contains("name can not be empty")
			);
		}

		
		[Test]
		public void GetName_creationWithSomeName_returnCorrectName()
		{
			Settlement settlement = Settlement.Create("my settlement");
			Assert.AreEqual("my settlement", settlement.Name);
		}

		
		[Test]
		public void GetSize_createNewSettlement_returnOne()
		{
			Settlement settlement = Settlement.Create("my settlement");
			Assert.AreEqual(1, settlement.Size);
		}
		
		[Test]
		public void SetSize_setPositiveSize_getSizeReturnCorrectValue()
		{
			Settlement settlement = Settlement.Create("my settlement");
			settlement.Size = 5;
			Assert.AreEqual(5, settlement.Size);
		}
		
		[Test]
		public void SetSize_setNegativeSize_throwsException()
		{
			Settlement settlement = Settlement.Create("my settlement");
			var exc = Assert.Throws<ArgumentException>(
				() => settlement.Size = -5
			);
			Assert.IsTrue(
				exc.Message.ToLower().Contains("settlement size can not be negative")
			);
		}
		
		[Test]
		public void SetSize_setZeroSize_getSizeReturnCorrectValue()
		{
			Settlement settlement = Settlement.Create("my settlement");
			settlement.Size = 0;
			Assert.AreEqual(0, settlement.Size);
		}

		
		[Test]
		public void IsAbandoned_createNewSettlement_isNotAbandoned()
		{
			Settlement settlement = Settlement.Create("my settlement");
			Assert.IsFalse(settlement.IsAbandoned);
		}
		
		[Test]
		public void IsAbandoned_setSizeToZero_isAbandoned()
		{
			Settlement settlement = Settlement.Create("my settlement");
			settlement.Size = 0;
			Assert.IsTrue(settlement.IsAbandoned);
		}
		
		[Test]
		public void IsAbandoned_setPositiveSize_isNotAbandoned()
		{
			Settlement settlement = Settlement.Create("my settlement");
			settlement.Size = 5;
			Assert.IsFalse(settlement.IsAbandoned);
		}

		
		[Test]
		public void GetMaxSizeInPast_createNewSettlement_returnOne()
		{
			Settlement settlement = Settlement.Create("my settlement");
			Assert.AreEqual(1, settlement.MaxSizeInPast);
		}
		
		[Test]
		public void GetMaxSizeInPast_setSizeOneTime_returnNewValue()
		{
			Settlement settlement = Settlement.Create("my settlement");
			settlement.Size = 5;
			Assert.AreEqual(5, settlement.MaxSizeInPast);
		}
		
		[Test]
		public void GetMaxSizeInPast_setSizeFirstGreater_returnGreater()
		{
			Settlement settlement = Settlement.Create("my settlement");
			settlement.Size = 5;
			settlement.Size = 3;
			settlement.Size = 4;
			Assert.AreEqual(5, settlement.MaxSizeInPast);
		}
		
		[Test]
		public void GetMaxSizeInPast_setSizeSecondGreater_returnGreater()
		{
			Settlement settlement = Settlement.Create("my settlement");
			settlement.Size = 3;
			settlement.Size = 5;
			settlement.Size = 4;
			Assert.AreEqual(5, settlement.MaxSizeInPast);
		}
		
		[Test]
		public void GetMaxSizeInPast_setSizeThirdGreater_returnGreater()
		{
			Settlement settlement = Settlement.Create("my settlement");
			settlement.Size = 3;
			settlement.Size = 4;
			settlement.Size = 5;
			Assert.AreEqual(5, settlement.MaxSizeInPast);
		}

		
		[Test]
		public void GetRaceWeight_createNewSettlement_throwsException()
		{
			Settlement settlement = Settlement.Create("my settlement");
			var exc = Assert.Throws<InvalidOperationException>(
				() => settlement.GetRaceWeight("human")
			);
			Assert.IsTrue(
				exc.Message.ToLower().Contains(
					"settlement state error: races not setupped"
				)
			);
		}
		
		[Test]
		public void SetRaceWeight_setWeightForOneRace_getRaceWeightReturnOk()
		{
			Settlement settlement = Settlement.Create("my settlement");
			settlement.SetRaceWeight("human", 1000);
			Assert.AreEqual(1000, settlement.GetRaceWeight("human"));
		}
		
		[Test]
		public void GetRaceWeight_getWeightUnexistingRace_returnZero()
		{
			Settlement settlement = Settlement.Create("my settlement");
			settlement.SetRaceWeight("human", 1000);
			Assert.AreEqual(0, settlement.GetRaceWeight("orc"));
		}
		
		[Test]
		public void GetRaceWeight_setWeightExistingRace_returnNewValue()
		{
			Settlement settlement = Settlement.Create("my settlement");
			settlement.SetRaceWeight("human", 1000);
			settlement.SetRaceWeight("human", 500);
			Assert.AreEqual(500, settlement.GetRaceWeight("human"));
		}
		
		[Test]
		public void SetRaceWeight_setNegativeWeight_throwsException()
		{
			Settlement settlement = Settlement.Create("my settlement");
			var exc = Assert.Throws<ArgumentException>(
				() => settlement.SetRaceWeight("human", -1000)
			);
			Assert.IsTrue(
				exc.Message.ToLower().Contains(
					"race weight can not be negative"
				)
			);
		}
		
		[Test]
		public void SetRaceWeight_existTwoRacesSetZeroWeightOne_isOk()
		{
			Settlement settlement = Settlement.Create("my settlement");
			settlement.SetRaceWeight("human", 1000);
			settlement.SetRaceWeight("dwarf", 2000);
			settlement.SetRaceWeight("human", 0);
			Assert.AreEqual(0, settlement.GetRaceWeight("human"));
			Assert.AreEqual(2000, settlement.GetRaceWeight("dwarf"));
		}
		
		[Test]
		public void SetRaceWeight_existTwoRacesSetZeroBoth_throwsException()
		{
			Settlement settlement = Settlement.Create("my settlement");
			settlement.SetRaceWeight("human", 1000);
			settlement.SetRaceWeight("dwarf", 2000);
			settlement.SetRaceWeight("human", 0);
			var exc = Assert.Throws<InvalidOperationException>(
				() => settlement.SetRaceWeight("dwarf", 0)
			);
			Assert.IsTrue(
				exc.Message.ToLower().Contains(
					"impossible remove last race"
				)
			);
		}
		
		[Test]
		public void SetRaceWeight_setOneRaceWeightToZero_throwsException()
		{
			Settlement settlement = Settlement.Create("my settlement");
			settlement.SetRaceWeight("human", 0);
			var exc = Assert.Throws<InvalidOperationException>(
				() => settlement.GetRaceWeight("human")
			);
			Assert.IsTrue(
				exc.Message.ToLower().Contains(
					"settlement state error: races not setupped"
				)
			);
		}
		
		[Test]
		public void SetRaceWeight_raceNameIsEmpty_throwsException()
		{
			Settlement settlement = Settlement.Create("my settlement");
			var exc = Assert.Throws<ArgumentException>(
				() => settlement.SetRaceWeight("", 1000)
			);
			Assert.IsTrue(
				exc.Message.ToLower().Contains(
					"race name can not be empty"
				)
			);
		}
		
		[Test]
		public void GetRaceWeight_raceNameIsEmpty_throwsException()
		{
			Settlement settlement = Settlement.Create("my settlement");
			settlement.SetRaceWeight("human", 1000);
			var exc = Assert.Throws<ArgumentException>(
				() => settlement.GetRaceWeight("")
			);
			Assert.IsTrue(
				exc.Message.ToLower().Contains(
					"race name can not be empty"
				)
			);
		}


		[Test]
		public void GetRacePercent_getPercentForNewSettlement_returnZero()
		{
			Settlement settlement = Settlement.Create("my settlement");
			Assert.AreEqual(0, settlement.GetRacePercent("human"));
		}
		[Test]
		public void GetRacePercent_getPercentForExistingUniqueRace_return1000000()
		{
			Settlement settlement = Settlement.Create("my settlement");
			settlement.SetRaceWeight("human", 1000);
			Assert.AreEqual(1000000, settlement.GetRacePercent("human"));
		}
		[Test]
		public void GetRacePercent_existTwoEqualRaces_return500000forBoth()
		{
			Settlement settlement = Settlement.Create("my settlement");
			settlement.SetRaceWeight("human", 1000);
			settlement.SetRaceWeight("orc", 1000);
			Assert.AreEqual(500000, settlement.GetRacePercent("human"));
			Assert.AreEqual(500000, settlement.GetRacePercent("orc"));
		}
		[Test]
		public void GetRacePercent_addTwoRacesThenRemoveOne_return1000000forRest()
		{
			Settlement settlement = Settlement.Create("my settlement");
			settlement.SetRaceWeight("human", 1000);
			settlement.SetRaceWeight("orc", 1000);
			settlement.SetRaceWeight("orc", 0);
			Assert.AreEqual(1000000, settlement.GetRacePercent("human"));
			Assert.AreEqual(0, settlement.GetRacePercent("orc"));
		}
		[Test]
		public void GetRacePercent_raceNameIsEmpty_throwsException()
		{
			Settlement settlement = Settlement.Create("my settlement");
			settlement.SetRaceWeight("human", 1000);
			var exc = Assert.Throws<ArgumentException>(
				() => settlement.GetRacePercent("")
			);
			Assert.IsTrue(
				exc.Message.ToLower().Contains(
					"race name can not be empty"
				)
			);
		}
		[Test]
		public void GetRacePercent_addTwoRaces100and200_return333333and666667()
		{
			Settlement settlement = Settlement.Create("my settlement");
			settlement.SetRaceWeight("human", 100);
			settlement.SetRaceWeight("orc", 200);
			Assert.AreEqual(333333, settlement.GetRacePercent("human"));
			Assert.AreEqual(666667, settlement.GetRacePercent("orc"));
		}

		[Test]
		public void GetRacePercents_newSettlement_returnEmptyDict()
		{
			Settlement settlement = Settlement.Create("my settlement");
			var percents = settlement.GetRacePercents();
			Assert.AreEqual(0, percents.Count);
		}
		[Test]
		public void GetRacePercents_addOneRace_returnCorrectDict()
		{
			Settlement settlement = Settlement.Create("my settlement");
			settlement.SetRaceWeight("human", 100);
			var percents = settlement.GetRacePercents();
			Assert.AreEqual(1, percents.Count);
			Assert.AreEqual(1000000, percents["human"]);
		}
		[Test]
		public void GetRacePercents_addTwoRace_returnCorrectDict()
		{
			Settlement settlement = Settlement.Create("my settlement");
			settlement.SetRaceWeight("human", 100);
			settlement.SetRaceWeight("orc", 200);
			var percents = settlement.GetRacePercents();
			Assert.AreEqual(2, percents.Count);
			Assert.AreEqual(333333, percents["human"]);
			Assert.AreEqual(666667, percents["orc"]);
		}
	}
}
