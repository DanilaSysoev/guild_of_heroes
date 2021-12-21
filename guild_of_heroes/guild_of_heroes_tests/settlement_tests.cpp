#include "pch.h"
#include "guild_of_heroes.hpp"

using namespace guild_of_heroes;

TEST(SettlementTests, create_creationWithSomeName_creationOk) {
	Settlement* settlement = Settlement::create("my settlement");
	EXPECT_NE(settlement, nullptr);
	delete settlement;
}
TEST(SettlementTests, create_creationWithEmptyName_throwsException) {
	EXPECT_THROW_WITH_MESSAGE(
		Settlement::create(""),
		std::invalid_argument, 
		"name can not be empty"
	);
}

TEST(SettlementTests, getName_creationWithSomeName_returnCorrectName) {
	Settlement* settlement = Settlement::create("my settlement");
	EXPECT_EQ(settlement->getName(), "my settlement");
	delete settlement;
}

TEST(SettlementTests, getSize_createNewSettlement_returnOne) {
	Settlement* settlement = Settlement::create("my settlement");
	EXPECT_EQ(settlement->getSize(), 1);
	delete settlement;
}
TEST(SettlementTests, setSize_setPositiveSize_getSizeReturnCorrectValue) {
	Settlement* settlement = Settlement::create("my settlement");
	settlement->setSize(5);
	EXPECT_EQ(settlement->getSize(), 5);
	delete settlement;
}
TEST(SettlementTests, setSize_setNegativeSize_throwsException) {
	Settlement* settlement = Settlement::create("my settlement");
	EXPECT_THROW_WITH_MESSAGE(settlement->setSize(-5), std::invalid_argument, "settlement size can not be negative");	
	delete settlement;
}
TEST(SettlementTests, setSize_setZeroSize_getSizeReturnCorrectValue) {
	Settlement* settlement = Settlement::create("my settlement");
	settlement->setSize(0);
	EXPECT_EQ(settlement->getSize(), 0);
	delete settlement;
}

TEST(SettlementTests, isAbandoned_createNewSettlement_isNotAbandoned) {
	Settlement* settlement = Settlement::create("my settlement");	
	EXPECT_FALSE(settlement->isAbandoned());
	delete settlement;
}
TEST(SettlementTests, isAbandoned_setSizeToZero_isAbandoned) {
	Settlement* settlement = Settlement::create("my settlement");
	settlement->setSize(0);
	EXPECT_TRUE(settlement->isAbandoned());
	delete settlement;
}
TEST(SettlementTests, isAbandoned_setPositiveSize_isNotAbandoned) {
	Settlement* settlement = Settlement::create("my settlement");
	settlement->setSize(5);
	EXPECT_FALSE(settlement->isAbandoned());
	delete settlement;
}

TEST(SettlementTests, getMaxSizeInPast_createNewSettlement_returnOne) {
	Settlement* settlement = Settlement::create("my settlement");
	EXPECT_EQ(settlement->getMaxSizeInPast(), 1);
	delete settlement;
}
TEST(SettlementTests, getMaxSizeInPast_setSizeOneTime_returnNewValue) {
	Settlement* settlement = Settlement::create("my settlement");
	settlement->setSize(5);
	EXPECT_EQ(settlement->getMaxSizeInPast(), 5);
	delete settlement;
}