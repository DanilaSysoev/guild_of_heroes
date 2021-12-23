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
TEST(GuildTests, getHeroes_addTwoHeroes_resultContainsBoth) {
	Guild* guild = Guild::create("my guild");
	Hero* hero1 = Hero::create("my hero 1");
	Hero* hero2 = Hero::create("my hero 2");
	guild->addHero(hero1);
	guild->addHero(hero2);

	auto heroes = guild->getHeroes();
	EXPECT_NE(std::find(heroes.begin(), heroes.end(), hero1), heroes.end());
	EXPECT_NE(std::find(heroes.begin(), heroes.end(), hero2), heroes.end());
	EXPECT_EQ(heroes.size(), 2);

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

TEST(GuildTests, removeHero_removeExistHero_heroesCountDecrement) {
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
TEST(GuildTests, removeHero_removeNonExistentHero_throwsException) {
	Guild* guild = Guild::create("my guild");
	Hero* hero1 = Hero::create("my hero 1");
	Hero* hero2 = Hero::create("my hero 2");
	guild->addHero(hero1);
	EXPECT_THROW_WITH_MESSAGE(
		guild->removeHero(hero2),
		std::logic_error,
		"attempt to delete non-existent hero"
	);
	delete guild;
	delete hero1;
	delete hero2;
}
TEST(GuildTests, removeHero_removeExistHero_getHeroesReturnOk) {
	Guild* guild = Guild::create("my guild");
	Hero* hero1 = Hero::create("my hero 1");
	Hero* hero2 = Hero::create("my hero 2");
	guild->addHero(hero1);
	guild->addHero(hero2);
	guild->removeHero(hero1);

	auto heroes = guild->getHeroes();
	EXPECT_NE(std::find(heroes.begin(), heroes.end(), hero2), heroes.end());
	EXPECT_EQ(std::find(heroes.begin(), heroes.end(), hero1), heroes.end());
	EXPECT_EQ(heroes.size(), 1);

	delete guild;
	delete hero1;
	delete hero2;
}

TEST(GuildTests, getHeroesDailyPayment_creatNewGuild_equalsZero) {
	Guild* guild = Guild::create("my guild");

	EXPECT_EQ(guild->getHeroesDailyPayment(), 0);

	delete guild;
}
TEST(GuildTests, getHeroesDailyPayment_twoHeroes_returnCorrect) {
	Guild* guild = Guild::create("my guild");
	Hero* hero1 = Hero::create("my hero 1");
	Hero* hero2 = Hero::create("my hero 2");
	hero1->setDailyFee(10);
	hero2->setDailyFee(20);
	guild->addHero(hero1);
	guild->addHero(hero2);

	EXPECT_EQ(guild->getHeroesDailyPayment(), 30);

	delete guild;
	delete hero1;
	delete hero2;
}
