#include "pch.h"
#include "guild_of_heroes.hpp"

TEST(GuildTests, create_creationWithSomeName_creationOk) {
	Guild* guild = Guild.create("my guild");
	EXPECT_NE(guild, nullptr);
}
