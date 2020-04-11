backend-run:
	dotnet run --project WhatsIn/WhatsIn/

frontend-run:
	npm start --prefix frontend

.PHONY: backend-run frontend-run
