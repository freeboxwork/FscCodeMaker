using System.Collections.Generic;
using UnityEngine;
using System;

namespace ApiResultData
{
    public class ModelClass : MonoBehaviour
    {


    }


    [Serializable]
    public class API_DATA
    {
        public Result result;
    }

    [Serializable]
    public class Result
    {
        public List<Product> products = new List<Product>();
    }

    [Serializable]
    public class Product
    {
        public string id;
        public string modelEnum;
        public string modelYearEnum;
        public string trimEnum;
        public string variantEnum;
        public string modelDisplayName;
        public string productid;
        public List<FscDependency> fscDependencies = new List<FscDependency>();
    }
    [Serializable]
    public class FscDependency
    {
        public string fsc;
        public List<SpecOption> specOptions = new List<SpecOption>();
        public List<ColorCombination> colorCombinations = new List<ColorCombination>();
    }

    [Serializable]
    public class ColorCombination
    {
        public string colorCombinationId;
        public List<Color> colors;
    }
    [Serializable]
    public class Color
    {
        public string colorId;
        public string colorCode;
        public string colorDisplayName;
    }

    [Serializable]
    public class SpecOption
    {
        public string categoryTypeEnum;
        public string categoryTypeDisplayName;
        public List<SpecOptionItem> specOptionItems = new List<SpecOptionItem>();
    }
    [Serializable]
    public class SpecOptionItem
    {
        public string id;
        public string itemCode;
    }
}
