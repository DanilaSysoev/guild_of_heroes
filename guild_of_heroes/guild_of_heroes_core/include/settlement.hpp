#pragma once
#include "nameable.hpp"

namespace guild_of_heroes {
	class Settlement : public Nameable {
	public:
		int getSize() const;
		int getMaxSizeInPast() const;
		void setSize(int size);
		bool isAbandoned() const;
		int getRaceWeight(const std::string& race);

		static Settlement* create(const std::string& name);

	private:
		Settlement(const std::string& name);

		int size;
		int maxSizeInPast;
	};
}