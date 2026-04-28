using System.Globalization;
using UnityEngine;

public static class Helpers
{
    private static Camera _camera;

    public static Camera Camera
    {
        get
        {
            if(_camera == null)
            {
                _camera = Camera.main;
            }
            return _camera;
        }
    }
    public static string NumberFormatter(int value)
    {
        if (value >= 1_000_000_000)
            return (value / 1_000_000_000f).ToString("0.#", CultureInfo.InvariantCulture) + "B";

        if (value >= 1_000_000)
            return (value / 1_000_000f).ToString("0.#", CultureInfo.InvariantCulture) + "M";

        if (value >= 1_000)
            return (value / 1_000f).ToString("0.#", CultureInfo.InvariantCulture) + "K";

        return value.ToString();
    }
}
