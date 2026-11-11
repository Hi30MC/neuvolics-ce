dotnet build ./Neuvolics

cp tasks.txt ../../neuvolics_test_env/tasks.txt
(cd ../../neuvolics_test_env && ./OpusMutatum)
