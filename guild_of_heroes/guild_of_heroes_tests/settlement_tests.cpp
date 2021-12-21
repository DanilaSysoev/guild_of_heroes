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