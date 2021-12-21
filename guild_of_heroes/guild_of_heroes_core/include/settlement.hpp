#pragma once
#include "nameable.hpp"

namespace guild_of_heroes {
	class Settlement : public Nameable {
	public:
		int getSize() const;
		void setSize(int size);

		static Settlement* create(const std::string& name);

	private:
		Settlement(const std::string& name);

		int size;
	};
}