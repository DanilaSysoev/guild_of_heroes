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
		public void create_creationWithSomeName_creationOk()
		{
			Settlement settlement = Settlement.Create("my settlement");
			Assert.IsNotNull(settlement);
		}

		[Test]
		public void create_creationWithEmptyName_throwsException()
		{
			var exc = Assert.Throws<ArgumentException>(
				() => Settlement.Create("")
			);
			Assert.IsTrue(
				exc.Message.ToLower().Contains("name can not be empty")
			);
		}

		
		[Test]
		public void getName_creationWithSomeName_returnCorrectName()
		{
			Settlement settlement = Settlement.Create("my settlement");
			Assert.AreEqual("my settlement", settlement.Name);
		}

		
		[Test]
		public void getSize_createNewSettlement_returnOne()
		{
			Settlement settlement = Settlement.Create("my settlement");
			Assert.AreEqual(1, settlement.Size);
		}
		
		[Test]
		public void setSize_setPositiveSize_getSizeReturnCorrectValue()
		{
			Settlement settlement = Settlement.Create("my settlement");
			settlement.Size = 5;
			Assert.AreEqual(5, settlement.Size);
		}
		
		[Test]
		public void setSize_setNegativeSize_throwsException()
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
		public void setSize_setZeroSize_getSizeReturnCorrectValue()
		{
			Settlement settlement = Settlement.Create("my settlement");
			settlement.Size = 0;
			Assert.AreEqual(0, settlement.Size);
		}

		
		[Test]
		public void isAbandoned_createNewSettlement_isNotAbandoned()
		{
			Settlement settlement = Settlement.Create("my settlement");
			Assert.IsFalse(settlement.IsAbandoned);
		}
		
		[Test]
		public void isAbandoned_setSizeToZero_isAbandoned()
		{
			Settlement settlement = Settlement.Create("my settlement");
			settlement.Size = 0;
			Assert.IsTrue(settlement.IsAbandoned);
		}
		
		[Test]
		public void isAbandoned_setPositiveSize_isNotAbandoned()
		{
			Settlement settlement = Settlement.Create("my settlement");
			settlement.Size = 5;
			Assert.IsFalse(settlement.IsAbandoned);
		}

		
		[Test]
		public void getMaxSizeInPast_createNewSettlement_returnOne()
		{
			Settlement settlement = Settlement.Create("my settlement");
			Assert.AreEqual(1, settlement.MaxSizeInPast);
		}
		
		[Test]
		public void getMaxSizeInPast_setSizeOneTime_returnNewValue()
		{
			Settlement settlement = Settlement.Create("my settlement");
			settlement.Size = 5;
			Assert.AreEqual(5, settlement.MaxSizeInPast);
		}
		
		[Test]
		public void getMaxSizeInPast_setSizeFirstGreater_returnGreater()
		{
			Settlement settlement = Settlement.Create("my settlement");
			settlement.Size = 5;
			settlement.Size = 3;
			settlement.Size = 4;
			Assert.AreEqual(5, settlement.MaxSizeInPast);
		}
		
		[Test]
		public void getMaxSizeInPast_setSizeSecondGreater_returnGreater()
		{
			Settlement settlement = Settlement.Create("my settlement");
			settlement.Size = 3;
			settlement.Size = 5;
			settlement.Size = 4;
			Assert.AreEqual(5, settlement.MaxSizeInPast);
		}
		
		[Test]
		public void getMaxSizeInPast_setSizeThirdGreater_returnGreater()
		{
			Settlement settlement = Settlement.Create("my settlement");
			settlement.Size = 3;
			settlement.Size = 4;
			settlement.Size = 5;
			Assert.AreEqual(5, settlement.MaxSizeInPast);
		}

		
		[Test]
		public void getRaceWeight_createNewSettlement_throwsException()
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
		public void setRaceWeight_setWeightForOneRace_getRaceWeightReturnOk()
		{
			Settlement settlement = Settlement.Create("my settlement");
			settlement.SetRaceWeight("human", 1000);
			Assert.AreEqual(1000, settlement.GetRaceWeight("human"));
		}
		
		[Test]
		public void getRaceWeight_getWeightUnexistingRace_returnZero()
		{
			Settlement settlement = Settlement.Create("my settlement");
			settlement.SetRaceWeight("human", 1000);
			Assert.AreEqual(0, settlement.GetRaceWeight("orc"));
		}
		
		[Test]
		public void getRaceWeight_setWeightExistingRace_returnNewValue()
		{
			Settlement settlement = Settlement.Create("my settlement");
			settlement.SetRaceWeight("human", 1000);
			settlement.SetRaceWeight("human", 500);
			Assert.AreEqual(500, settlement.GetRaceWeight("human"));
		}
		
		[Test]
		public void setRaceWeight_setNegativeWeight_throwsException()
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
		public void setRaceWeight_existTwoRacesSetZeroWeightOne_isOk()
		{
			Settlement settlement = Settlement.Create("my settlement");
			settlement.SetRaceWeight("human", 1000);
			settlement.SetRaceWeight("dwarf", 2000);
			settlement.SetRaceWeight("human", 0);
			Assert.AreEqual(0, settlement.GetRaceWeight("human"));
			Assert.AreEqual(2000, settlement.GetRaceWeight("dwarf"));
		}
		
		[Test]
		public void setRaceWeight_existTwoRacesSetZeroBoth_throwsException()
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
		public void setRaceWeight_setOneRaceWeightToZero_throwsException()
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
		public void setRaceWeight_raceNameIsEmpty_throwsException()
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
		public void getRaceWeight_raceNameIsEmpty_throwsException()
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
	}
}
