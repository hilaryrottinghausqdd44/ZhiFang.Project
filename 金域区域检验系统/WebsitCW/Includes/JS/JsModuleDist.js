
///
///////////////////////////////
// 模块刷新脚本(ModuleDist)：//
// 北京智方科技开发有限公司  //
// 时间:2009-02-16           //
///////////////////////////////
// 从一个界面刷新另一个界面是所调用的参数
// 返回参数，string 描述调用操作结果
//转移符规则，符号复制,如[;]变为[;;]  [,]变为[,,]

// 1 ModuleNoAndName:模块编号与名称 (来至ModuleArgPrv:id,Name) ModuleArgPrv
// 
// 2 ModuleUrl:模块地址
//*3 ModuleRefArg:关联输出参数名称
//*4 ModuleRefArgValue:关联输出参数内容

//*5 OutPutParaUrlParas: 刷新其他模块参数(OutPutParaUrl), 
//               格式：=同界面1(frmID1),[同界面1url],[Name1],{string1};
//                      同界面2(frmID2),[同界面2url],[Name2],{string2};
//                      [_Blank],弹出界面url,[ID1],{string1}
//url涉及到& 或 (&amp;) 符号时, 换成＃

// 6 WebsiteLocationUrl: 该模块的全部地址
// 7 OthersParas: 扩展参数
// 8 DebugPara 调试参数(true, false);默认为false;可以显示传入参数的全部内容

//--------示例：ModuleDist('500,新闻系统',
//       '/2008/Doc/News.aspx?newsCata=国际新闻&其他=其他',
//       '新闻编号,新闻标题',
//       '29,国际大事',
//      'frm1,/2008/doc/NewsText.aspx,NewsID,{新闻编号:29};[height=500:width=600:status=yes:toolbar=no:menubar=no:location=no:scrollbars=yes],/2008/doc/NewsText.aspx?sf=p&i=p,NewsID,{新闻标题:[]};',
//      '',
//      '',
//       false)
///
function ModuleDist(
    ModuleNoAndName,
    ModuleUrl,
    ModuleRefArg,
    ModuleRefArgValue,
    OutPutParaUrlParas,
    WebsiteLocationUrl,
    OthersParas,
    DebugPara) {

    //alert("ModuleDist CALL:\n" + OutPutParaUrlParas);
    
    //调试
    if (DebugPara) {
        alert('方法调用调试：\n' + '模块编号与名称：ModuleNoAndName'+ ModuleNoAndName + '\n' +
            '关联输出参数名称：ModuleRefArg=' + ModuleRefArg + '\n' +
            '关联输出参数内容：ModuleRefArgValue=' + ModuleRefArgValue + '\n' +
            '刷新其他模块参数：OutPutParaUrlParas=' + OutPutParaUrlParas + '\n' +
            '模块的全部地址：WebsiteLocationUrl=' + WebsiteLocationUrl + '\n' +
            '扩展参数：OthersParas=' + OthersParas + '\n' +
            '调试参数：DebugPara=' + DebugPara);
    }

    var iTargetRedirect = 0;
    var iTargetRedirectAll = 0;

    var arrModuleRefArg = SplitString(ModuleRefArg, ',');
    var arrModuleRefArgValue = SplitString(ModuleRefArgValue, ',');
    
    
    //循环刷新或打开窗口
    if (OutPutParaUrlParas != null
        && OutPutParaUrlParas.length > 0
        && arrModuleRefArg != null
        && arrModuleRefArgValue != null
        && arrModuleRefArg.length==arrModuleRefArgValue.length) {
        //全部参数，按;分号折分数组
        var arrayOutPutParaUrlParas = SplitString(OutPutParaUrlParas, ';');
        if (arrayOutPutParaUrlParas != null && arrayOutPutParaUrlParas.length > 0) {
            iTargetRedirectAll = arrayOutPutParaUrlParas.length;
            for (var i = 0; i < arrayOutPutParaUrlParas.length; i++) {
                ////每个参数，按,逗号折分数组
                var EachArrayOutPutParaUrlParas = SplitString(arrayOutPutParaUrlParas[i], ',');
                if (EachArrayOutPutParaUrlParas != null && EachArrayOutPutParaUrlParas.length > 3) {
                    var strFrameID = EachArrayOutPutParaUrlParas[0];
                    var strTargetUrl = EachArrayOutPutParaUrlParas[1];
                    strTargetUrl = strTargetUrl.replace(/＃/g, '&');
                    strTargetUrl = strTargetUrl.replace(/&amp;/g, '&');
                    strTargetUrl = strTargetUrl.replace(/ =/g, '=');
                    
                    var strInputArg = EachArrayOutPutParaUrlParas[2];
                    var strInputValue = EachArrayOutPutParaUrlParas[3];
                    
                    //是否要对比目标窗口的地址
                    var bNeedCompareTargetUrl = false;
                    bNeedCompareTargetUrl = CompareTargetUrl(strFrameID, arrayOutPutParaUrlParas);
                    
                    

                    if (strInputValue.indexOf('{') == 0 && strInputValue.length>2)//and lastIndexOf('}')=strInputValue.length-1)
                    {
                        var strInputValueEnds = strInputValue.substr(1, strInputValue.length - 2);
                        var arrRelavantPara = SplitString(strInputValueEnds, ':');
                        
                        //根据值刷新或弹出窗口
                        if (arrRelavantPara.length > 1) {
                            var strOutPutValue = '';
                            for (var iModuleRefArgValue = 0; iModuleRefArgValue < arrModuleRefArgValue.length; iModuleRefArgValue++) {
                                if (arrModuleRefArg[iModuleRefArgValue] == arrRelavantPara[0]
                                    && arrModuleRefArgValue[iModuleRefArgValue] == arrRelavantPara[1]) {
                                    strOutPutValue = arrModuleRefArgValue[iModuleRefArgValue];
                                    break;
                                }
                            }
                            

                            if (strOutPutValue.length > 0) {
//                                if (bNeedCompareTargetUrl) {
//                                    var bCompareTargetUrlResult = false;
//                                    bCompareTargetUrlResult = CompareTargetUrlResult(strFrameID, arrayOutPutParaUrlParas, strOutPutValue);
//                                    if (!bCompareTargetUrlResult)
//                                        continue;
//                                }
                                OpenOrRedirect(strFrameID, strTargetUrl, strInputArg, strInputValue, strOutPutValue);
                                iTargetRedirect++;
                            }
                        }
                        else //根据参数刷新或弹出窗口
                        {
                            var strOutPutValue = '';
                            for (var iModuleRefArgValue = 0; iModuleRefArgValue < arrModuleRefArgValue.length; iModuleRefArgValue++) {
                                if (arrModuleRefArg[iModuleRefArgValue] == arrRelavantPara[0]) {
                                    strOutPutValue = arrModuleRefArgValue[iModuleRefArgValue];
                                    break;
                                }
                            }
                            if (strOutPutValue.length > 0) {
                                if (bNeedCompareTargetUrl && (strFrameID.length != 0 && strFrameID.indexOf('[') < 0)) {
                                    var bCompareTargetUrlResult = false;
                                    bCompareTargetUrlResult = CompareTargetUrlResult(strFrameID, strTargetUrl);
                                    if (!bCompareTargetUrlResult)
                                        continue;
                                }
                                OpenOrRedirect(strFrameID, strTargetUrl, strInputArg, strInputValue, strOutPutValue);
                                iTargetRedirect++;
                            }
                        }
                    }
                    else {
                        alert('刷新或弹出窗口 ('+i+') 的第四个参数设置不正确，格式应该为 {关联参数名[:关联参数值]}\n或是参数中有(,)逗号没有转移');
                    }

                    
                }
            }
        }
    }
    else
        alert('没有传入所需要的 \n关联输出参数名称：ModuleRefArg=' + ModuleRefArg + '\n' +
            '关联输出参数内容：ModuleRefArgValue=' + ModuleRefArgValue + '\n' +
            '刷新其他模块参数：OutPutParaUrlParas=' + OutPutParaUrlParas + '\n' +'参数，无法刷新或打开其他窗口');

    //调试
    if (DebugPara) {
        alert('调试显示，共刷新或打开了' + iTargetRedirect + '/' + iTargetRedirectAll + '个窗口');
    }
}

function SplitString(strSource, strSeparator) {
    if (strSource == null || strSource.length == 0)
        return null;
    if (strSource.indexOf(strSeparator) < 0) {
        return strSource.split(strSeparator);
    }
    
    var reg=new RegExp(strSeparator + strSeparator,"g"); //创建正则RegExp对象


    var strSourceTemp = strSource;
    strSourceTemp = strSourceTemp.replace(reg, '\v');
    var arraySource = strSourceTemp.split(strSeparator);
    if (arraySource != null) {
        for (var i = 0; i < arraySource.length; i++) {
            arraySource[i] = arraySource[i].replace(/\v/g, strSeparator);
        }
    }

    return arraySource;
}

function OpenOrRedirect(strFrameID, strTargetUrl, strInputArg, strInputValue, strOutPutValue) {
    var ConnectPara='?';
    if (strTargetUrl.indexOf('?') > 0) {
        ConnectPara = '&';
    }
    
    //&转移符＃的还原
    strTargetUrl = strTargetUrl.replace(/＃/g, '&');
    strTargetUrl = strTargetUrl.replace(/&amp;/g, '&');
    
    //判断=前面是否有空格
    strTargetUrl=strTargetUrl.replace(/ =/g,'=');

    var iInputArgBegins=strTargetUrl.indexOf(strInputArg + '=');
    if (iInputArgBegins > 0) {
        var strTargetUrlTemp = strTargetUrl.substr(0, iInputArgBegins);
        if (strTargetUrl.indexOf('&', iInputArgBegins, 1) > 0) {
            strTargetUrlTemp += strTargetUrl.substr(strTargetUrl.indexOf('&', iInputArgBegins, 1) + 1);

            strTargetUrl = strTargetUrlTemp;
        }
    }
     
    if (strFrameID.indexOf('[') < 0 && strFrameID.length>0) {
        var frmRedirect = parent.frames[strFrameID];
        if (frmRedirect) {
            frmRedirect.document.location = strTargetUrl + ConnectPara + strInputArg + '=' + strOutPutValue;
        }
        else {
            alert('没有找到' + strFrameID + '框架,请检验配置文件');
        }
    }
    else{
        //height=500:width=600:status=yes:toolbar=no:menubar=no:location=no:scrollbars=yes:resizable=yes
        var dialogParas = 'height=500:width=600:status=yes:toolbar=no:menubar=no:location=no:scrollbars=yes:resizable=yes';
        if(strFrameID.indexOf('[')==0){
            dialogParas=strFrameID.substr(1,strFrameID.length-1);
        }
        dialogParas=dialogParas.replace(/:/g,',');
        window.open(strTargetUrl + ConnectPara + strInputArg + '=' + strOutPutValue, '_blank', dialogParas);
    }
}

//是否有一个窗口的多个刷新调用
function CompareTargetUrl(strFrameID, arrayOutPutParaUrlParas) {
    var bCompareTargetUrl = false;
    var iCompared=0;
    for (var i = 0; i < arrayOutPutParaUrlParas.length; i++) {
        var EachArrayOutPutParaUrlParas = SplitString(arrayOutPutParaUrlParas[i], ',');
        if (strFrameID == EachArrayOutPutParaUrlParas[0]) {
            iCompared++;
        }
    }
    if (iCompared > 1) {
        bCompareTargetUrl = true;
    }
    return bCompareTargetUrl;
}

function CompareTargetUrlResult(strFrameID, strTargetUrlSource) {
    var bReturn = false;
    var frmRedirect = parent.frames[strFrameID];
    if (frmRedirect) {
        var strTargetUrlDestination=frmRedirect.document.location.href;
        strTargetUrlSource=strTargetUrlSource.toLowerCase();
        strTargetUrlDestination=strTargetUrlDestination.toLowerCase();
            
        if (strTargetUrlSource == strTargetUrlDestination) {
            bReturn = true;
        }
        else {
            var iStrTargetUrlSource=strTargetUrlSource.indexOf('.aspx');
            if (iStrTargetUrlSource > 0) {
                //比较/website.aspx
                var strTargetUrlSourceTemp = strTargetUrlSource.substr(0, iStrTargetUrlSource) + '.aspx';
                if (strTargetUrlSourceTemp.indexOf('/') > 0) {
                    strTargetUrlSourceTemp = strTargetUrlSourceTemp.substr(strTargetUrlSourceTemp.laseIndexOf('/'));
                    if (strTargetUrlDestination.indexOf(strTargetUrlSourceTemp) > 0) {
                        if (SearchArrays(strTargetUrlDestination,strTargetUrlSource, 'name=,db=,TableCName='))
                            bReturn = true;
                    }
                }
            }
        }
    }
    else
        return bReturn;
}


function SearchArrays(strTargetUrlDestination, strTargetUrlSource, strIndexArray) {
    var bReturn = false;
    if (strIndexArray != null && strIndexArray.length > 0) {
        var arrayIndex = strIndexArray.split(',');

        var bSourceFound = false;
        var bDestinationFound = false;
        
        for (var i = 0; i < arrayIndex.length; i++) {
            if (strTargetUrlSource.indexOf(arrayIndex[i]) > 0) {
                bSourceFound = true;
            }
            if (strTargetUrlDestination.indexOf(arrayIndex[i]) > 0) {
                bDestinationFound = true;
            }
        }
        if (!bSourceFound)
            return true;
            
        if (bDestinationFound == bSourceFound) {
            for (var i = 0; i < arrayIndex.length; i++) {
                if (strTargetUrlSource.indexOf(arrayIndex[i]) > 0) {
                    var strSourceTemp = strTargetUrlSource.substr(arrayIndex[i])
                    if (strSourceTemp.indexOf('&') > 0) {
                        strSourceTemp = strSourceTemp.substr(0, strSourceTemp.indexOf('&'));
                    }
                    if (strTargetUrlDestination.indexOf(strSourceTemp) < 0) {
                        return false;
                    }
                }
            }
            return true;
        }
    }
    return bReturn;
}



//'GridViewUserControl,/OA2008/DataInput/BrowseGridViewFormUseFrame.aspx?name=用户资源管理＃dbName=用户资源管理＃TableEName=CustomersData'
//通过一个条件刷新别的窗口,比如查询条件
function RefreshRelationModalDataByHZF(pkSQL)
{
    //alert("RefreshRelationModalDataByHZF:" + pkSQL);
    var iframeUrl = window.document.location.href;
    //alert(iframeUrl);
    //从 url 中取关联参数的值
    var allParam = iframeUrl.substring(iframeUrl.indexOf("?")+1);
    var splitParams = allParam.split("&");//取到参数列表并拆分
    //定义参数
    var OutPutParaUrl = "";//关联参数的值
    for(var i=0;i<splitParams.length;i++)
    {
        var splitParam = splitParams[i].split("=");//拆分当前参数参数
        var paramName = splitParam[0];
        //alert("paramName:" + paramName);
        var paramValue = splitParams[i].replace(paramName + "=", "");
        //alert("paramValue:" + paramValue);
        if(paramName == "OutPutParaUrl")
        {
            OutPutParaUrl = paramValue;//关联参数的值
            break;
        }
    }
    //alert("OutPutParaUrl:\n" + OutPutParaUrl);
    //刷新
    ModuleDist('401,数据查询',
          '/Oa2008/DataInput/BrowseQueryFormUseFrame.aspx?para=systemName=用户资源管理,dbName=用户资源管理,tableName=CustomersData',
          'pkSQL',
          pkSQL,
          OutPutParaUrl + ',pkSQL,{pkSQL}',
          '',
          '',
           false);
}
