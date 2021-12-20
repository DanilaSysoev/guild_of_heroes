#include "pch.h"
#include "guild_of_heroes.hpp"

using namespace guild_of_heroes;

TEST(HeroTests, create_creationWithSomeName_creationOk) {
	Hero* hero = Hero::create("my hero");
	EXPECT_NE(hero, nullptr);
	delete hero;
}