#pragma once
#include<string>

namespace guild_of_heroes {
	class Guild {
	public:
		static Guild* create(const std::string& name);

	private:
		Guild(const std::string& name);

		std::string name;
	};
}