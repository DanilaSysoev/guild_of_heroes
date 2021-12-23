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
TEST(GuildTests, create_creationWithSomeName_heroesListIsEmpty) {
	Guild* guild = Guild::create("my guild");
	EXPECT_EQ(guild->getHeroes(), std::vector<Hero*>());
	delete guild;
}

TEST(GuildTests, getName_creationWithSomeName_returnCorrectName) {
	Guild* guild = Guild::create("my guild");
	EXPECT_EQ(guild->getName(), "my guild");
	delete guild;
}

TEST(GuildTests, addHero_addFirstHero_heroesCountOne) {
	Guild* guild = Guild::create("my guild");
	Hero* hero = Hero::create("my hero");
	guild->addHero(hero);
	EXPECT_EQ(guild->getHeroesCount(), 1);
	delete guild;
	delete hero;
}
TEST(GuildTests, addHero_addSecondHero_heroesCountIncrement) {
	Guild* guild = Guild::create("my guild");
	Hero* hero1 = Hero::create("my hero 1");
	Hero* hero2 = Hero::create("my hero 2");
	guild->addHero(hero1);
	EXPECT_EQ(guild->getHeroesCount(), 1);
	guild->addHero(hero2);
	EXPECT_EQ(guild->getHeroesCount(), 2);
	delete guild;
	delete hero1;
	delete hero2;
}
TEST(GuildTests, addHero_addNullHero_throwsException) {
	Guild* guild = Guild::create("my guild");	
	EXPECT_THROW_WITH_MESSAGE(
		guild->addHero(nullptr),
		std::invalid_argument,
		"hero can not be null"
	);
	delete guild;
}

TEST(GuildTests, removeHero_removeExistHeroHero_heroesCountDecrement) {
	Guild* guild = Guild::create("my guild");
	Hero* hero1 = Hero::create("my hero 1");
	Hero* hero2 = Hero::create("my hero 2");
	guild->addHero(hero1);
	guild->addHero(hero2);
	EXPECT_EQ(guild->getHeroesCount(), 2);
	guild->removeHero(hero1);
	EXPECT_EQ(guild->getHeroesCount(), 1);
	delete guild;
	delete hero1;
	delete hero2;
}