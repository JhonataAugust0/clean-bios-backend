# Define a imagem base
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

# Define o diretório de trabalho
WORKDIR /app

# Copia o arquivo csproj e restaura as dependências
COPY *.csproj ./
RUN dotnet restore

# Copia todos os arquivos e compila o projeto
COPY . ./

# COPY settings.json /

RUN dotnet publish -c Release -o out

# Define a imagem de tempo de execução
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .

# Expõe a porta do aplicativo
EXPOSE 80

# Define o comando de inicialização
ENTRYPOINT ["dotnet", "CleanBios.dll"]
