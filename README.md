# Jaomix-Downloader

## Работоспобность проверена на Windows 11 и Debian 11 (bullseye)

## Установка Windows

Установите pandoc последней версии (https://pandoc.org/installing.html)

Установите chromium или chrome (если вдруг версия выше 105, то воспользуйтесь инструкцией ниже) 

Распакуйте архив в папку и запустите .exe

## Установка Linux

Установите pandoc последней версии (https://pandoc.org/installing.html)

Установите chromium или chrome (если вдруг версия выше 105, то воспользуйтесь инструкцией ниже) 

Распакуйте архив в папку

Сделайте файл JaomixDownloader исполняемым:
chmod +x JaomixDownloader

Запустите JaomixDownloader:
./JaomixDownloader

## Использование кода

Установить следующие nuget пакеты:

~~~
HtmlAgilityPack
Selenium.WebDriver
Selenium.WebDriver.ChromeDriver
System.Configuration.ConfigurationManager
YamlDotNet
~~~~

## Если вдруг появляется ошибка с chromedriver из-за более новой версии:
Скачайте под нужную версию хрома с https://chromedriver.chromium.org/downloads и замените его в папке


