import os
import requests

API_KEY = os.environ.get("OPENWEATHER_API_KEY")

def get_weather():
    params = {
        "lat": "55.752121",
        "lon": "37.617664",
        "appid": API_KEY,
        "units": "metric",
        "lang": "ru"
    }
    url = "https://api.openweathermap.org/data/2.5/weather"
    try:
        response = requests.get(url, params=params, timeout=10)
        response.raise_for_status()
        data = response.json()
        if data["cod"] != 200:
            print(f"Ошибка API: {data.get('message', 'Неизвестная ошибка')}")
            return None
        return data
    except requests.exceptions.RequestException as e:
        print(f"Ошибка сети: {e}")
        return None

def main():
    weather = get_weather()
    if not weather:
        print("Не удалось получить погоду.")
        return

    city_name = weather.get("name", "Москва")
    main_info = weather.get("main", {})
    temp = main_info.get("temp")
    feels_like = main_info.get("feels_like")
    humidity = main_info.get("humidity")
    pressure = main_info.get("pressure")
    description = weather.get("weather", [])
    weather_desc = description[0].get("description")
    wind = weather.get("wind", {})
    wind_speed = wind.get("speed")

    print(f"Погода в городе {city_name}")
    print(f"Температура: {temp}°C")
    print(f"Ощущается как: {feels_like}°C")
    print(f"Описание: {weather_desc}")
    print(f"Влажность: {humidity}%")
    print(f"Давление: {pressure} гПа")
    print(f"Скорость ветра: {wind_speed} м/с")

main()