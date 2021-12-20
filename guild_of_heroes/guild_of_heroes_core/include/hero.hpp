#pragma once
#include <string>

namespace guild_of_heroes {
	class Hero {
	public:
		std::string getName() const;

		static Hero* create(const std::string& name);


	private:
		Hero(const std::string& name);

		std::string name;
	};
}