#include "pch.h"
#include "guild_of_heroes.hpp"

using namespace guild_of_heroes;

TEST(HeroTests, create_creationWithSomeName_creationOk) {
	Hero* hero = Hero::create("my hero");
	EXPECT_NE(hero, nullptr);
	delete hero;
}
TEST(HeroTests, create_creationWithEmptyName_throwsException) {
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
TEST(HeroTests, addSkill_addSomeSkill_skillsCountEqualsOne) {
	Hero* hero = Hero::create("my hero");
	hero->addSkill("some skill", 1);
	EXPECT_EQ(hero->getSkillsCount(), 1);
	delete hero;
}
TEST(HeroTests, addSkill_addSkillWithEmptyName_throwsException) {
	Hero* hero = Hero::create("my hero");
	EXPECT_THROW(hero->addSkill("", 1), std::invalid_argument);	
	delete hero;
}
TEST(HeroTests, addSkill_addSkillWithNegativeValue_throwsException) {
	Hero* hero = Hero::create("my hero");
	EXPECT_THROW(hero->addSkill("some skill", -1), std::invalid_argument);
	delete hero;
}
TEST(HeroTests, addSkill_addSkillWithSomeValue_getSkillReturnCorrectValue) {
	Hero* hero = Hero::create("my hero");
	hero->addSkill("some skill", 1);
	EXPECT_EQ(hero->getSkill("some skill"), 1);
	delete hero;
}
TEST(HeroTests, addSkill_addSkillWithExistingName_rewriteOldValue) {
	Hero* hero = Hero::create("my hero");
	hero->addSkill("some skill", 1);
	hero->addSkill("some skill", 2);
	EXPECT_EQ(hero->getSkill("some skill"), 2);
	delete hero;
}
TEST(HeroTests, getSkill_getSkillWithUnexistingName_returnZero) {
	Hero* hero = Hero::create("my hero");
	EXPECT_EQ(hero->getSkill("some skill"), 0);
	delete hero;
}

TEST(HeroTests, getSkills_getSkillsForEmptyHero_returnEmptyMap) {
	Hero* hero = Hero::create("my hero");
	EXPECT_TRUE(hero->getSkills().empty());
	delete hero;
}
TEST(HeroTests, getSkills_addOneSkill_returnCorrectMap) {
	Hero* hero = Hero::create("my hero");
	hero->addSkill("some skill", 10);
	auto skills = hero->getSkills();
	EXPECT_EQ(skills.size(), 1);
	EXPECT_EQ(skills["some skill"], 10);
	EXPECT_EQ(skills.size(), 1);
	delete hero;
}
TEST(HeroTests, getSkills_addTwoSkills_returnCorrectMap) {
	Hero* hero = Hero::create("my hero");
	hero->addSkill("skill 1", 10);
	hero->addSkill("skill 2", 20);
	auto skills = hero->getSkills();
	EXPECT_EQ(skills.size(), 2);
	EXPECT_EQ(skills["skill 1"], 10);
	EXPECT_EQ(skills.size(), 2);
	EXPECT_EQ(skills["skill 2"], 20);
	EXPECT_EQ(skills.size(), 2);
	delete hero;
}