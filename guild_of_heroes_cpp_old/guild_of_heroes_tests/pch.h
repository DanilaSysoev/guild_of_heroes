//
// pch.h
//

#pragma once

#include <string>
#include <algorithm>
#include <stdexcept>
#include "gtest/gtest.h"

#define EXPECT_THROW_WITH_MESSAGE(statement, exception, message) \
	try {\
		(statement);\
		FAIL() << "Error: expect " << #exception; \
	} catch (const exception& ex) {\
		std::string lowercase_what(ex.what());\
		std::transform(lowercase_what.begin(), lowercase_what.end(), lowercase_what.begin(), \
					  [](unsigned char c){ return std::tolower(c); });\
		std::string exp_msg(message);\
		std::transform(exp_msg.begin(), exp_msg.end(), exp_msg.begin(), \
					  [](unsigned char c){ return std::tolower(c); });\
		EXPECT_TRUE(lowercase_what.find(exp_msg) != std::string::npos);\
	} catch (...) {\
		FAIL() << "Error: expect " << #exception; \
	}