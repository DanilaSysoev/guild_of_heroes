#pragma once
#include <string>

namespace guild_of_heroes {
	class Nameable {
	public:
		std::string getName() const;

		virtual ~Nameable();

	protected:
		Nameable(const std::string& name);

	private:
		std::string name;
	};
}