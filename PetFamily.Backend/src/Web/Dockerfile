FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Web/Web.csproj", "src/Web/"]
COPY ["src/Accounts/PetFamily.Accounts.Presentation/PetFamily.Accounts.Presentation.csproj", "src/Accounts/PetFamily.Accounts.Presentation/"]
COPY ["src/Shared/PetFamily.Core/PetFamily.Core.csproj", "src/Shared/PetFamily.Core/"]
COPY ["src/Accounts/PetFamily.Accounts.Domain/PetFamily.Accounts.Domain.csproj", "src/Accounts/PetFamily.Accounts.Domain/"]
COPY ["src/Shared/PetFamily.SharedKernel/PetFamily.SharedKernel.csproj", "src/Shared/PetFamily.SharedKernel/"]
COPY ["src/VolunteerRequest/PetFamily.VolunteerRequest.Domain/PetFamily.VolunteerRequest.Domain.csproj", "src/VolunteerRequest/PetFamily.VolunteerRequest.Domain/"]
COPY ["src/Shared/PetFamily.Framework/PetFamily.Framework.csproj", "src/Shared/PetFamily.Framework/"]
COPY ["src/Accounts/PetFamily.Accounts.Contracts/PetFamily.Accounts.Contracts.csproj", "src/Accounts/PetFamily.Accounts.Contracts/"]
COPY ["src/Accounts/PetFamily.Accounts.Application/PetFamily.Accounts.Application.csproj", "src/Accounts/PetFamily.Accounts.Application/"]
COPY ["src/Accounts/PetFamily.Accounts.Infrastructure/PetFamily.Accounts.Infrastructure.csproj", "src/Accounts/PetFamily.Accounts.Infrastructure/"]
COPY ["src/Discussion/PerFamily.Discussion.Infrastructure/PerFamily.Discussion.Infrastructure.csproj", "src/Discussion/PerFamily.Discussion.Infrastructure/"]
COPY ["src/Discussion/PerFamily.Discussion.Application/PerFamily.Discussion.Application.csproj", "src/Discussion/PerFamily.Discussion.Application/"]
COPY ["src/Discussion/PerFamily.Discussion.Domain/PerFamily.Discussion.Domain.csproj", "src/Discussion/PerFamily.Discussion.Domain/"]
COPY ["src/Discussion/PerFamily.Discussion.Presentation/PerFamily.Discussion.Presentation.csproj", "src/Discussion/PerFamily.Discussion.Presentation/"]
COPY ["src/Discussion/PerFamily.Discussion.Contracts/PerFamily.Discussion.Contracts.csproj", "src/Discussion/PerFamily.Discussion.Contracts/"]
COPY ["src/Species/PetFamily.Species.Presentation/PetFamily.Species.Presentation.csproj", "src/Species/PetFamily.Species.Presentation/"]
COPY ["src/Species/PetFamily.Species.Application/PetFamily.Species.Application.csproj", "src/Species/PetFamily.Species.Application/"]
COPY ["src/VolunteerManagement/PetFamily.VolunteerManagement.Contracts/PetFamily.VolunteerManagement.Contracts.csproj", "src/VolunteerManagement/PetFamily.VolunteerManagement.Contracts/"]
COPY ["src/Species/PetFamily.Species.Domain/PetFamily.Species.Domain.csproj", "src/Species/PetFamily.Species.Domain/"]
COPY ["src/Species/PetFamily.Species.Contracts/PetFamily.Species.Contracts.csproj", "src/Species/PetFamily.Species.Contracts/"]
COPY ["src/Species/PetFamily.Species.Infrastructure/PetFamily.Species.Infrastructure.csproj", "src/Species/PetFamily.Species.Infrastructure/"]
COPY ["src/VolunteerManagement/PetFamily.VolunteerManagement.Presentation/PetFamily.VolunteerManagement.Presentation.csproj", "src/VolunteerManagement/PetFamily.VolunteerManagement.Presentation/"]
COPY ["src/VolunteerManagement/PetFamily.VolunteerManagement.Application/PetFamily.VolunteerManagement.Application.csproj", "src/VolunteerManagement/PetFamily.VolunteerManagement.Application/"]
COPY ["src/VolunteerManagement/PetFamily.VolunteerManagement.Domain/PetFamily.VolunteerManagement.Domain.csproj", "src/VolunteerManagement/PetFamily.VolunteerManagement.Domain/"]
COPY ["src/VolunteerManagement/PetFamily.VolunteerManagement.Infrastructure/PetFamily.VolunteerManagement.Infrastructure.csproj", "src/VolunteerManagement/PetFamily.VolunteerManagement.Infrastructure/"]
COPY ["src/VolunteerRequest/PetFamily.VolunteerRequest.Infrastructure/PetFamily.VolunteerRequest.Infrastructure.csproj", "src/VolunteerRequest/PetFamily.VolunteerRequest.Infrastructure/"]
COPY ["src/VolunteerRequest/PetFamily.VolunteerRequest.Application/PetFamily.VolunteerRequest.Application.csproj", "src/VolunteerRequest/PetFamily.VolunteerRequest.Application/"]
COPY ["src/VolunteerRequest/PetFamily.VolunteerRequest.Presentation/PetFamily.VolunteerRequest.Presentation.csproj", "src/VolunteerRequest/PetFamily.VolunteerRequest.Presentation/"]
COPY ["src/VolunteerRequest/PetFamily.VolunteerRequest.Contracts/PetFamily.VolunteerRequest.Contracts.csproj", "src/VolunteerRequest/PetFamily.VolunteerRequest.Contracts/"]
RUN dotnet restore "src/Web/Web.csproj"
COPY . .
WORKDIR "/src/src/Web"
RUN dotnet build "Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.dll"]
