#include "pch.h"
#include "guild_of_heroes.hpp"

using namespace guild_of_heroes;

TEST(GuildTests, create_creationWithSomeName_creationOk) {
	Guild* guild = Guild::create("my guild");
	EXPECT_NE(guild, nullptr);
	delete guild;
}
TEST(GuildTests, create_creationWithEmptyName_throwsException) {
	EXPECT_THROW_WITH_MESSAGE(
		Guild::create(""), 
		std::invalid_argument,
		"name can not be empty"
	);
}
TEST(GuildTests, create_creationWithSomeName_heroesCountZero) {
	Guild* guild = Guild::create("my guild");
	EXPECT_EQ(guild->getHeroesCount(), 0);
	delete guild;
}

TEST(GuildTests, getName_creationWithSomeName_returnCorrectName) {
	Guild* guild = Guild::create("my guild");
	EXPECT_EQ(guild->getName(), "my guild");
	delete guild;
}

