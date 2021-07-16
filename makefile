all: restore build test

restore:
	dotnet restore

build:
	dotnet build --no-restore --nologo

test:
	dotnet test --no-build --verbosity normal --nologo