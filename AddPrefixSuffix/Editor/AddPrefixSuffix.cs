//Hierarchyで選択したゲームオブジェクトの先頭や末尾に文字列を追加できるエディタ拡張
//アバター改変するときに衣装のボーンのGameobjectに名前付けると後から見てどの衣装のボーンなのかわかりやすくするためのやつ

//unity version: 2018.4.20f1
//2021/02/13

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; //エディタ拡張用のライブラリ

public class AddPrefixSuffix : ScriptableWizard
{
    [SerializeField] string headString; //先頭に追加する文字列
    [SerializeField] string tailString; //末尾に追加する文字列

    [MenuItem("Editor/AddPrefixSuffix")]
    private static void CreateWizard()
    {
        //ウィンドウとボタンの生成
        ScriptableWizard.DisplayWizard("AddPrefixSuffix to Gameobject", typeof(AddPrefixSuffix), "! delete !", "rename");
    }

    //!delete!ボタンが押された時呼ばれる
    void OnWizardCreate(){
        deleteRename();
    }

    //renameボタンが押された時呼ばれる
    void OnWizardOtherButton(){
        rename();
    }


    //先頭と末尾に指定文字列を追加する
    void rename()
    {
        GameObject[] targetGameObject = Selection.gameObjects;

        foreach(GameObject go in targetGameObject)
        {
            if(!string.IsNullOrEmpty(headString)){
                go.name = headString + go.name;
            }
            if(!string.IsNullOrEmpty(tailString)){
                go.name = go.name + tailString;
            }
        }
    }

    //文字列一致で先頭と末尾を削除する
    void deleteRename(){
        GameObject[] targetGameObject = Selection.gameObjects;
        foreach(GameObject go in targetGameObject)
        {
            //先頭に指定文字列がある場合にReplaceで削除 (indexofは検索文字列がnullだと0を返すのでnull判定もする)
            if(go.name.IndexOf(headString) == 0 && !string.IsNullOrEmpty(headString)){
                go.name = go.name.Replace(headString,"");
            }

            //末尾に指定文字列がある場合にReplaceで削除 (全体の文字数 - 検索文字数 = 検索文字数が見つかる先頭の文字数)
            if(go.name.IndexOf(tailString) == (go.name.Length - tailString.Length) && !string.IsNullOrEmpty(tailString)){
                go.name = go.name.Replace(tailString,"");    
            }
            //go.name = go.name.Replace(headString,"");
            //go.name = go.name.Replace(tailString,"");
        }
    }


}
