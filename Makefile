
default: run

run: backend-run

backend-run:
	dotnet run --project WhatsIn/WhatsIn/

.PHONY: backend-run
