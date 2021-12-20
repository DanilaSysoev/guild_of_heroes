#include "pch.h"
#include "guild_of_heroes.hpp"

using namespace guild_of_heroes;

TEST(HeroTests, create_creationWithSomeName_creationOk) {
	Hero* hero = Hero::create("my hero");
	EXPECT_NE(hero, nullptr);
	delete hero;
}
TEST(HeroTests, create_creationWithEmptyName_throwException) {
	EXPECT_THROW(Hero::create(""), std::invalid_argument);
}
TEST(HeroTests, create_creationWithSomeName_skillsCountEqualsZero) {
	Hero* hero = Hero::create("my hero");
	EXPECT_EQ(hero->getSkillsCount(), 0);
	delete hero;
}

TEST(HeroTests, getName_creationWithSomeName_returnCorrectName) {
	Hero* hero = Hero::create("my hero");
	EXPECT_EQ(hero->getName(), "my hero");
	delete hero;
}
