using UnityEngine;

namespace LPTask.Utils
{
    public sealed class PlayerPrefsDataHolder
        : IDataHolder<string>
    {
        public PlayerPrefsDataHolder(string key)
        {
            _key = key;
        }

        private readonly string _key;
        
        private string _cached;
        
        public bool TryGetData(out string data)
        {
            data = string.Empty;
            
            if (_cached != null)
            {
                data = _cached;
                return true;
            }

            if (PlayerPrefs.HasKey(_key))
            {
                data = PlayerPrefs.GetString(_key);
                return true;
            }

            return false;
        }

        public void SetData(string data)
        {
            _cached = data;
                
            PlayerPrefs.SetString(_key, data);
            PlayerPrefs.Save();
        }
    }
}