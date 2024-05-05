#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80
EXPOSE 666

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# copy the class library project
COPY ["./MSP.Common/MSP.Common.csproj", "MSP.Common/"]
COPY ["./MSP.Coupon/MSP.Coupon.csproj", "MSP.Coupon/"]

RUN dotnet restore "MSP.Coupon/MSP.Coupon.csproj"

COPY . .

WORKDIR "/src/MSP.Coupon"
RUN dotnet build "MSP.Coupon.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MSP.Coupon.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MSP.Coupon.dll"]