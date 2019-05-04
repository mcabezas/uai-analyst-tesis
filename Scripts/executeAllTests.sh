#!/usr/bin/env bash

DOTNET_ROOT=$HOME/dotnet/dotnet-sdk-2.2.203-linux-x64
TESTER_SUITE_DIR=/$HOME/UAI/Diploma/uaitesis/Src/TesterSuite/bin/Debug/netcoreapp2.2

${DOTNET_ROOT}/dotnet ${TESTER_SUITE_DIR}/TesterSuite.dll $1

