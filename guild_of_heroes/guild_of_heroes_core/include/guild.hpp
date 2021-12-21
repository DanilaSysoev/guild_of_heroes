#pragma once
#include "nameable.hpp"

namespace guild_of_heroes {
	class Guild : public Nameable {
	public:
		static Guild* create(const std::string& name);


	private:
		Guild(const std::string& name);
	};
}