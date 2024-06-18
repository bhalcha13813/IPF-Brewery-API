# IPF-Brewery-API

Brewery API is used to add update retrieve Breweries, Bars, Beers, map Bar-Beers & map Brewery-Beers

## Framework
This API is written in Dot Net Core 6.0

## Architecture
- This API is developed with the code first approach.
- API has multi layers as following
  * Brewery API 
  * Service Layer, 
  * Data Layer

### Local machine set-up 
- clone IPF-Brewery-API repository
- build solution
- Update Database Connection string in appsettings.json
- Go to Package Manager Console
- Execute Update-Database command
- Brewery database will be created
- Set IPF.Brewery.API as startup project
- Run the API
- You will be redirected to Swagger endpoint where all API endpoints can be executed
