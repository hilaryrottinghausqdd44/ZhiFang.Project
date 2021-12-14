/**
	@name：出厂设置
	@author：liangyl
	@version 2020-02-04
 */
layui.extend({
	multiSelect:'ux/other/multiSelect',
}).define(['form','colorpicker','laydate','multiSelect'],function(exports){
	"use strict";
	
	var $=layui.$,
		uxutil = layui.uxutil,
		colorpicker = layui.colorpicker,
		laydate = layui.laydate,
		multiSelect = layui.multiSelect,
		form = layui.form;
	
    //生成的颜色框ID,需要逐个绑定
    var COLORLIST =[];
     //时间类型变量ID,需要逐个绑定
    var DADELIST =[];
    //数字类型变量ID,需要逐个绑定
    var NUMBERLIST =[];
    //获取出厂设置参数服务
    var SELECT_FACTORY_URL = "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_QueryFactorySettingPara?isPlanish=true"; 
	 //获取默认设置参数服务
    var SELECT_DEFEAULT_URL = "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_QuerySystemDefaultPara?isPlanish=true"; 

	var paraform={
		//全局项
		config:{
		},
		//设置全局项
		set:function(options){
			var me = this;
			me.config = $.extend({},me.config,options);
			return me;
		}
	};
	//构造器
	var Class = function(setings){  
		var me = this;
		me.config = $.extend({},me.config,paraform.config,setings);
	};
	Class.pt = Class.prototype;
     //创建组件
    Class.pt.createControl = function(list,divid,formID){
    	var me = this;
    	COLORLIST=[];
    	NUMBERLIST=[];
    	var HTML ='<div class="layui-form" id="'+formID+'" lay-filter="'+formID+'"  style="white-space:nowrap;overflow:auto;"> '+
	        '<div class="layui-row" id="'+formID+'_row">';
	        
	    //计算layui-form-label 显示长度,取最长的宽带
	    var strArr = [],
			textLength = 0,textWidth=0;

		for(var i = 0; i < list.length; i++) {
			if(list[i].BPara_CName || list[i].BPara_CName.length>0){
				var  strlength=me.getNum(list[i].BPara_CName);
				strArr.push(strlength);
			}
		}
		//项目(layui-form-label)text最大长度
		if(strArr.length>0)textLength = Math.max.apply(null, strArr);
	    textWidth=Number(textLength)*14;
	    
    	for(var i=0;i<list.length;i++){
    		var ParaEditInfo = list[i].BPara_ParaEditInfo;
    		var valType = ParaEditInfo.toUpperCase();
    		//如果存在多个值，截取第一个|前的内容
    		if(ParaEditInfo.length>1){
    			var str = ParaEditInfo.split('|');
    			valType = str[0];
    		}
    		HTML+='<div class=" layui-col-md12  layui-col-sm12 layui-col-xs12">'
    		switch (valType){
    			case 'D':
    			    HTML+= me.createDate(list[i],textWidth,'D',formID);
    				break;
    			case 'T':
    			    HTML+= me.createDate(list[i],textWidth,'T',formID);
    				break;
    			case 'DT':
    			    HTML+= me.createDate(list[i],textWidth,'DT',formID);
    				break;
    			case 'E':
    			    HTML+= me.createCheckBox(list[i],textWidth,'E',formID);
    				break; 
    			case 'S':
    			    HTML+= me.createRadio(list[i],textWidth,'S',formID);
    				break;
    			case 'CL':
    			    HTML+= me.createComBo(list[i],textWidth,'CL',formID);
    				break;
    			case 'I':
    			    HTML+= me.createNumber(list[i],textWidth,'I',formID);
    				break;
    			case 'L':
    			    HTML+= me.createNumber(list[i],textWidth,'L',formID);
    				break;
    			case 'F':
    			    HTML+= me.createNumber(list[i],textWidth,'F',formID);
    				break;
    			case 'RGB':
    			    HTML+= me.createColor(list[i],textWidth,formID);
    				break;
    			default: //默认为文本框C
    			    HTML+= me.createTextBox(list[i],textWidth,'C',formID);
    				break;
    		}
    		HTML+='</div>';
		}
    	HTML+='</div><div class="layui-row" id="'+formID+'_toolbarbtn'+'"></div></div>';
    	$(divid).html(HTML);
    	form.render();
    	me.initListeners(list,formID);
    };

    //清空表单数据	
	Class.pt.clearData = function(divid){
		var me = this;
		var HTML ='<div class="layui-none">暂无数据</div>';
		$(divid).html(HTML);
	};
		//获取默认值
	Class.pt.getDefaultValue = function(obj){
		var value =  obj.BPara_ParaValue || "";
        return value;
	};
//	//获取默认值
//	Class.pt.getDefaultValue = function(obj){
//		var value ="";
//		var ParaEditInfo = obj.BPara_ParaEditInfo;
//		if(ParaEditInfo){
//			//第一个|后的内容为默认值,即第二个值
//			var arr = ParaEditInfo.split('|');
//			if(arr.length>1){
//				value = arr[1];
//			}
//		}
//      return value;
//	};
	/**
	 * 解析输入列表，返回值
	 *设计规则：数据类型代码|默认值|输入列表|下拉或展开(0下拉，1展开)
	 * */
	Class.pt.getCheckBoxCL = function(obj){
		var me = this;
		var value ="";
		var ParaEditInfo = obj.BPara_ParaEditInfo;
		//第二个|后的内容为默认值,即第二个值
		var arr = ParaEditInfo.split('|');
		if(arr.length>=2){
			value = arr[2];
		}
        return value;
	};
	//获取E类展现类型,下拉或展开(0下拉，1展开),
	Class.pt.IsCheckBoxShowMode = function(obj){
		var me = this;
		var value ="0";
		var ParaEditInfo = obj.BPara_ParaEditInfo;
		//第三个|后的内容为默认值,即第三个值
		var arr = ParaEditInfo.split('|');
		
		if(arr.length>=3){
			value = arr[3];
		}
        return value;
	};
	
	//获取E类 下拉方式返回html
	Class.pt.createEComboHtml = function(formID,obj,defaultValue){
		var me = this;
		var  html ='<select name="'+formID+'_'+obj.BPara_ParaNo+'" lay-filter="'+formID+'_'+obj.BPara_ParaNo+'">';
		var ParaEditInfo = obj.BPara_ParaEditInfo;
		//第三个|的内容为默认值,即第三个值
		var arr = ParaEditInfo.split('|');
		var value="";
		if(arr.length>=2)value = arr[2];
		if(value){
			var list = value.split('#');
			var checkedHtml="",checkedHtml1="";
			if(defaultValue=='1')checkedHtml = "selected";
			   else  
			       checkedHtml1 = "selected";
			html+= '<option value="1"'+checkedHtml+'>'+list[0]+'</option>'+
			    '<option value="0" '+checkedHtml1+' >'+list[1]+'</option>';
		}
		html+='</select>';
		return html;
	};
   //获取S类 下拉方式返回html
	Class.pt.createSComboHtml = function(formID,obj,defaultValue){
		var me = this;
		var  html ='<select name="'+formID+'_'+obj.BPara_ParaNo+'" lay-filter="'+formID+'_'+obj.BPara_ParaNo+'">';
		var ParaEditInfo = obj.BPara_ParaEditInfo;
		//第三个|的内容为默认值,即第三个值
		var arr = ParaEditInfo.split('|');
		var value="";
		if(arr.length>=2)value = arr[2];//下拉内容
		if(defaultValue=='0' || !defaultValue)html+='<option value="">请选择</option>';
		if(value){
			var list = value.split('#');
			for(var i = 0; i <list.length; i++) {
				var numstr = i+1;
				var checkedHtml = "";
				//存在默认值
				if(defaultValue== numstr && defaultValue){
					checkedHtml = "selected";
				}
				html+= '<option value="'+i+'">'+list[i]+'</option>';
			};
		}
		html+='</select>';
		return html;
	};
	/**
	 * * 创建下拉框，返回html
	 */
	Class.pt.createComboHtml =  function(obj,defaultValue,list,isdevelop,ismulselect,formID){
	    var me = this;
	    var  html ='<select name="'+formID+'_'+obj.BPara_ParaNo+'" id="'+formID+'_'+obj.BPara_ParaNo+'" lay-filter="'+formID+'_'+obj.BPara_ParaNo+'" ><option value="">请选择</option>';
	    
	    if(!isdevelop && ismulselect ){//多选
	    	html ='<select name="'+formID+'_'+obj.BPara_ParaNo+'" id="'+formID+'_'+obj.BPara_ParaNo+'" multiple  lay-filter="'+formID+'_'+obj.BPara_ParaNo+'"> ';
	    }
		var ParaEditInfo = obj.BPara_ParaEditInfo;
		for(var i = 0; i <list.length; i++) {
			var checkedHtml = "";
			//存在默认值
			if(defaultValue== list[i]){
				checkedHtml = "selected";
			}
			var arr = list[i].split("/") || [];
			html+= '<option value="'+arr[0]+'" '+checkedHtml+'>'+ arr[0] +'</option>';
		}
		html+='</select>';
		return html;
	};

	/**
	 * 创建下拉组件(暂不支持下拉输入模式2021-09-22)
	 * 设计规则：数据类型|默认值|长度|下拉列表|输入框高度
	 * 下拉列表格式：下拉列表/可输入结果/ 下拉或展开/列表项可多选
	 *	1.可输入结果：1为可手工输入结果，0为只能从列表项中选择结果
	 *	2.下拉或展开：1为展开，0为下拉（默认）
	 *	3.列表项多选：1为可以选择多个列表项的值，0为只选择其中一项（默认）
	 * @param {Object} obj 
	 * @param {Object} textLength   lable 长度
	 * @param {Object} type  类型
	 */
	Class.pt.createComBo = function(obj,textLength,type,formID){
		var me = this;
		//默认值
    	var defaultValue = me.getDefaultValue(obj);
    	//下拉列表返回值(下拉内容),第四个值|
    	var arr = obj.BPara_ParaEditInfo.split('|');
    	var strval = "";
    	if(arr.length>=3)strval = arr[3];//第四个数组
    	//下拉内容
    	var list = strval.split('#') || [];
    	//是否允许输入，false 不允许
    	var isinput = false;
    	//是否允许多选,默认单选
    	var ismulselect = false;
    	//是否是展开方式显示,默认不展开 ，展开方式多选采用多选框展开;展开方式单选，采用单选组展开
    	var isdevelop = false;
    	//下拉内容

        //获取最后一个下拉内容的值,判断是否存在其他规则
        if(list.length>0){
        	//取字符/后的规则
        	var rulearr = list[list.length-1].split("/");
        	//可输入结果：1为可手工输入结果，0为只能从列表项中选择结果
        	if(rulearr[1]=='1' && rulearr[1])isinput=true;
        	//下拉或展开：1为展开，0为下拉（默认）
        	if(rulearr[2]=='1' && rulearr[2])isdevelop=true;
        	//列表项多选：1为可以选择多个列表项的值，0为只选择其中一项（默认
        	if(rulearr[3]=='1' && rulearr[3])ismulselect=true;
        }
//      console.log('可输入结果'+isinput+'下拉或展开'+isdevelop+'列表项多选'+ismulselect+'是否是展开方式'+isdevelop);

		var selectHtml =  me.createComboHtml(obj,defaultValue,list,isdevelop,ismulselect,formID);
//		//可输入下拉框
//		if(isinput && !isdevelop){
//			var inputHhtml = '<input type="text" id="'+formID+'_'+'Text_'+obj.BPara_ParaNo+'"  name="'+formID+'_'+'Text_'+obj.BPara_ParaNo+'" autocomplete="off" class="layui-input inputSelect" />';
//			selectHtml=inputHhtml+selectHtml;
//			console.log(selectHtml);
//		}
		var btnTextHtml = '采用出厂设置';
        if(formID=='Personality')btnTextHtml='采用默认值';
		var html = '<div class="layui-form-item">'+  
           '<label class="layui-form-label" style="width:'+textLength+'px">'+obj.BPara_CName+'</label>'+  
           '<div class="layui-input-block">'+ 
            '<div class="layui-input-inline">'+  
	          selectHtml+
            '</div>'+  
            '<div class="layui-inline layui-hide">'+  
             '<button class="layui-btn layui-btn-xs" id="'+formID+'_'+'btn_'+obj.BPara_ParaNo+'"><i class="iconfont">&#xe603;</i>&nbsp;'+btnTextHtml+'</button>'+
            '</div>'+
           '</div>'+  
         '</div>' ;
		return html;
	};
	//获取E类单选组
	Class.pt.getCheckBoxERadioList = function(formID,obj,defaultValue){
		var me = this;
		var  html ='';
		var ParaEditInfo = obj.BPara_ParaEditInfo;
		//第三个|的内容为默认值,即第三个值
		var arr = ParaEditInfo.split('|');
		var value="";
		if(arr.length>=2)value = arr[2];
		if(!value)return;
		var list = value.split('#');
		var checkedHtml1 = "",checkedHtml0="";
		//不设置默认值，默认为否
		if(!defaultValue)defaultValue=0;
		if(defaultValue== 0)checkedHtml0 = "checked";
		if(defaultValue== 1)checkedHtml1 = "checked";
		//默认值：1为执行，0为默认或不执行
		html= '<input type="radio"' +' lay-filter="'+formID+'_'+obj.BPara_ParaNo+'" name="'+formID+'_'+obj.BPara_ParaNo+'" value="1" title="'+list[0]+'" ' +checkedHtml1+'>'+
		    '<input type="radio"' +' lay-filter="'+formID+'_'+obj.BPara_ParaNo+'" name="'+formID+'_'+obj.BPara_ParaNo+'" value="0" title="'+list[1]+'" ' +checkedHtml0+'>';
		return html;
	};
	//获取S类单选组
	Class.pt.getCheckBoxSRadioList = function(formID,obj,defaultValue){
		var me = this;
		var  html ='';
		var ParaEditInfo = obj.BPara_ParaEditInfo;
		//第三个|的内容为默认值,即第三个值
		var arr = ParaEditInfo.split('|');
		var value="";
		if(arr.length>=2)value = arr[2];
		if(value){
			var list = value.split('#');
			for(var i = 0; i <list.length; i++) {
				var numstr = i+1;
				var checkedHtml = "";
				//存在默认值
				if(defaultValue== numstr && defaultValue){
					checkedHtml = "checked";
				}
				html+= '<input type="radio" name="'+formID+'_'+obj.BPara_ParaNo+'" value="'+numstr+'" title="'+list[i]+'" ' +checkedHtml+' lay-filter="'+formID+'_'+obj.BPara_ParaNo+'" >';
			};
		}
		return html;
	};
	//设置C类型的输入框高度(高度/宽度)
	Class.pt.IsTextBoxHight = function(obj){
		var me = this;
		//C类型高度在C类型规则中第四个|后的内容,也就是第五个数组
		var arr = obj.BPara_ParaEditInfo.split('|');
		var value="";
		if(arr.length>=4)value = arr[4];
		return value;
	};
     /**
	  * 创建文本时间类型Html
	  * 设计规则：数据类型|默认值|有效期
	  * @param {Object} obj 
	  * @param {Object} defaultValue  默认值
	*/
	Class.pt.createDateHtml = function(formID,obj,defaultValue,placeholder){
		var me = this;//lay-verify="date"
		var  html = 
		    '<input type="text" name="'+formID+'_'+obj.BPara_ParaNo+'" id="'+formID+'_'+obj.BPara_ParaNo+'" placeholder="'+placeholder+'" autocomplete="off" class="layui-input">';
        return html;
	};
	
	  /**
	  * 创建数字类型Html
	  * @param {Object} obj 
	  * @param {Object} defaultValue  默认值
	*/
	Class.pt.createNumHtml = function(formID,obj,obj2){
		var me = this;
        var defaultValue = obj2.DefaultValue;
        if(obj2.Type=='F' && obj2.DefaultValue){
        	defaultValue = me.returnFloat(defaultValue);
        }
        var Decimal = "";
        if(obj2.Type == "F" && !obj2.Decimal)obj2.Decimal=2;
        var html = 
            '<input type="number" autocomplete="off" class="layui-input" id="'+formID+'_'+obj.BPara_ParaNo+'" name="'+formID+'_'+obj.BPara_ParaNo+'" value="'+defaultValue+'" max="'+obj2.Max+'" min="'+obj2.Min+'" step="'+obj2.Step+'" Decimal="'+obj2.Decimal+'" >'; 
        return html;
	};
	/** 
	  * 创建E类展开方式单选 RadioHtml
	  * 设计规则：数据类型代码|默认值|输入列表|下拉或展开
	  * @param {Object} obj 
	  * @param {Object} defaultValue  默认值
	*/
	Class.pt.createERadioHtml = function(formID,obj,defaultValue){
		var me = this;
		var  html =  me.getCheckBoxERadioList(formID,obj,defaultValue);
        return html;
	};
	/** 
	  * 创建S类展开方式单选 RadioHtml
	  * 设计规则：数据类型代码|默认值|输入列表|下拉或展开
	  * @param {Object} obj 
	  * @param {Object} defaultValue  默认值
	*/
	Class.pt.createSRadioHtml = function(formID,obj,defaultValue){
		var me = this;
		var  html =  me.getCheckBoxSRadioList(formID,obj,defaultValue);
        return html;
	};
	/** 
	  * 创建文本框Html
	  * 设计规则：数据类型|默认值|长度|下拉列表|输入框高度(高度/宽度)
	  * 不支持长度,下拉列表，输入框高度(高度/宽度)
	  * @param {Object} obj 
	  * @param {Object} defaultValue  默认值
	*/
	Class.pt.createTextBoxHtml = function(formID,obj,defaultValue){
		var me = this;
		var  html = 
		    '<input type="text" name="'+formID+'_'+obj.BPara_ParaNo+'" autocomplete="off" placeholder="" value="'+defaultValue+'" class="layui-input" />';
        return html;
	};
	/**
	  * 创建文本域Html
	  * 设计规则：数据类型|默认值|长度|下拉列表|输入框高度(高度/宽度)
	  * 不支持长度,下拉列表，支持输入框高度(高度/宽度)
	  * @param {Object} obj 
	  * @param {Object} defaultValue  默认值
	*/
	Class.pt.createTextAreaHtml = function(formID,obj,defaultValue,value){
		var me = this;
		//解析高度和宽度
    	var arr = value.split('/');
		var  html = 
            '<textarea  name="'+formID+'_'+obj.BPara_ParaNo+'" id="'+formID+'_'+obj.BPara_ParaNo+'" placeholder="" class="layui-textarea" value="'+defaultValue+'"  style="width:'+arr[1]+'px;height:'+arr[0]+'px"></textarea>';
        return html;
	};
	
	/**
	 * @param {Object} obj 
	 * @param {Object} textLength   lable 长度
	 * @param {Object} type  类型
	 */
	Class.pt.createTextBox = function(obj,textLength,type,formID){
		var me = this;
		//默认值
    	var defaultValue = me.getDefaultValue(obj);
    	//是否存在输入高度设置
    	var hightWidth = me.IsTextBoxHight(obj);
    	var inputHtml = me.createTextBoxHtml(formID,obj,defaultValue);
    	if(hightWidth) inputHtml = me.createTextAreaHtml(formID,obj,defaultValue,hightWidth);
        var btnTextHtml = '采用出厂设置';
        if(formID=='Personality')btnTextHtml='采用默认值';
        var btnHtml = 'layui-hide';
		var html = '<div class="layui-form-item">'+  
           '<label class="layui-form-label" style="width:'+textLength+'px">'+obj.BPara_CName+'</label>'+  
           '<div class="layui-input-block">'+  
            '<div class="layui-input-inline">'+  
             inputHtml+
            '</div>'+
             '<div class="layui-inline layui-hide">'+  
             '<button class="layui-btn layui-btn-xs" id="'+formID+'_'+'btn_'+obj.BPara_ParaNo+'"><i class="iconfont">&#xe603;</i>&nbsp;'+btnTextHtml+'</button>'+
            '</div>'+
           '</div>'+  
         '</div>' ; 
         return html;
	};
	/**
	 * 创建时间类型
	 * 设计规则: 数据类型|默认值|有效期
	 * @param {Object} obj 
	 * @param {Object} textLength   lable 长度
	 * @param {Object} type  类型
	 */
	Class.pt.createDate = function(obj,textLength,type,formID){
		var me = this;
		//默认值
    	var defaultValue = me.getDefaultValue(obj);
    	//默认值：代码为CT，取当前日期或时间
    	if(defaultValue.toUpperCase()=="CT"){
    		var sysdate = uxutil.date.getDate();
    		defaultValue = uxutil.date.toString(sysdate,true);
    	}
    	var dateHtml = "",dateobj={Id:formID+'_'+obj.BPara_ParaNo};
		switch (type){
			case 'T':
			   dateobj.type = 'time';
			   dateHtml = me.createDateHtml(formID,obj,defaultValue,"HH:mm:ss");
				break;
			case 'DT':
			   dateobj.type = 'datetime';
			   dateHtml = me.createDateHtml(formID,obj,defaultValue,"yyyy-MM-dd HH:mm:ss");
			   break;
			default: //默认为D
			    dateobj.type = 'date';
			    dateHtml = me.createDateHtml(formID,obj,defaultValue,"yyyy-MM-dd");
				break;
		}
		//存储时间类型Id和内容方便渲染
		DADELIST.push(dateobj);
		var btnTextHtml = '采用出厂设置';
        if(formID=='Personality')btnTextHtml='采用默认值';
		var html = '<div class="layui-form-item">'+  
           '<label class="layui-form-label" style="width:'+textLength+'px">'+obj.BPara_CName+'</label>'+  
           '<div class="layui-input-block">'+  
            '<div class="layui-input-inline">'+  
             dateHtml+
            '</div>'+  
             '<div class="layui-inline layui-hide">'+  
             '<button class="layui-btn layui-btn-xs" id="'+formID+'_'+'btn_'+obj.BPara_ParaNo+'"><i class="iconfont">&#xe603;</i>&nbsp;'+btnTextHtml+'</button>'+
            '</div>'+
           '</div>'+  
         '</div>' ; 
         
         return html;
	};
	/**
	 * 创建数字类型
	 * 设计规则: 数据类型|默认值|最小值|最大值|小数位数
	 * @param {Object} obj 
	 * @param {Object} textLength   lable 长度
	 * @param {Object} type  类型
	 */
	Class.pt.createNumber = function(obj,textLength,type,formID){
		var me = this;
		//默认值
    	var defaultValue = me.getDefaultValue(obj);
    	
    	var ParaEditInfo = obj.BPara_ParaEditInfo;
        //解析获取规则
		var arr = ParaEditInfo.split('|');
		//第三个数组为最小值
		var minValue = arr[2] || '' ;
		//第三个数组为最大值
		var maxValue = arr[3] || '' ;
		//第四个数组为小数位数
		var decimalLentgh = arr[4] || '' ;
		//第⑤个数组为累计步距
		var step = arr[5] || '' ;
		var objNUM = {
			Id:obj.BPara_ParaNo,
			Min:minValue,
			Max:maxValue,
			Decimal:decimalLentgh,
			Type:type,
			DefaultValue:defaultValue,
			Step:step
		};
		NUMBERLIST.push(objNUM);
        var numberHtml = me.createNumHtml(formID,obj,objNUM);
		var btnTextHtml = '采用出厂设置';
        if(formID=='Personality')btnTextHtml='采用默认值';
		var html = '<div class="layui-form-item">'+
           '<label class="layui-form-label" style="width:'+textLength+'px">'+obj.BPara_CName+'</label>'+  
           '<div class="layui-input-block">'+  
            '<div class="layui-input-inline">'+  
              numberHtml+
            '</div>'+  
             '<div class="layui-inline layui-hide">'+
             '<button class="layui-btn layui-btn-xs" id="'+formID+'_'+'btn_'+obj.BPara_ParaNo+'"><i class="iconfont">&#xe603;</i>&nbsp;'+btnTextHtml+'</button>'+
            '</div>'+
           '</div>'+  
            
         '</div>' ; 
         return html;
	};
	/**
	  * 创建RadioHtml
	   *设计规则：数据类型代码|默认值|输入列表|下拉或展开(0下拉，1展开)
	   * 默认值：1为正常，2异常，0为默认即未检查
	  * @param {Object} obj 
	  * @param {Object} defaultValue  默认值
	*/
	Class.pt.createRadioHtml = function(formID,obj,defaultValue){
		var me = this;
		var radioHtml = "",radioHtml2="";
		//存在正常默认值
		if(defaultValue=="1")radioHtml = "checked";
		//存在异常默认值
		if(defaultValue=="2")radioHtml2 = "checked";
		var  html = 
            '<input type="radio"  name="'+formID+'_'+obj.BPara_ParaNo+'" value="1" title="正常" lay-skin="primary" '+radioHtml+'  lay-filter="'+formID+'_'+obj.BPara_ParaNo+'">'+
            '<input type="radio"  name="'+formID+'_'+obj.BPara_ParaNo+'" value="2" title="异常" lay-skin="primary" '+radioHtml2+' lay-filter="'+formID+'_'+obj.BPara_ParaNo+'">';
        return html;
	};
	/** 创建checkboxHtml
	   *设计规则：数据类型代码|默认值|输入列表|下拉或展开(0下拉，1展开)
	  * @param {Object} obj 
	  * @param {Object} defaultValue  默认值
	*/
	Class.pt.createCheckBoxHtml = function(formID,obj,defaultValue){
		var me = this;
		var checkedHtml = "";
		//存在默认值
		if(defaultValue=="1")checkedHtml = "checked";
		var  html = 
             '<input type="checkbox"  name="'+formID+'_'+obj.BPara_ParaNo+'" lay-skin="primary" '+checkedHtml+'  lay-filter="'+formID+'_'+obj.BPara_ParaNo+'">';
        return html;
	};
	/** 创建复选框
	  *设计规则：数据类型代码|默认值|输入列表|下拉或展开(0下拉，1展开)
	 * @param {Object} obj 
	 * @param {Object} textLength   lable 长度
	 * @param {Object} type  类型
	 */
	Class.pt.createCheckBox = function(obj,textLength,type,formID){
		var me = this;
    	//默认值(不选)
    	var defaultValue = me.getDefaultValue(obj);
    	//输入列表不为空
    	var CL = me.getCheckBoxCL(obj);
    	if(CL){
    		//判断是展开方式还是下拉方式(0下拉，1展开）
			var showMode = me.IsCheckBoxShowMode(obj);
			if(showMode){
				if(showMode =='1')var checkedHtml = me.createERadioHtml(formID,obj,defaultValue);
				else 
				   var checkedHtml = me.createEComboHtml(formID,obj,defaultValue);
			}else{ //只有输入列表，没有展开和下拉
				 var checkedHtml = me.createERadioHtml(formID,obj,defaultValue);
			}
    	}else{
    		var checkedHtml = me.createCheckBoxHtml(formID,obj,defaultValue);
    	}
    
    	var btnTextHtml = '采用出厂设置';
        if(formID=='Personality')btnTextHtml='采用默认值';
		var html = '<div class="layui-form-item">'+  
           '<label class="layui-form-label" style="width:'+textLength+'px">'+obj.BPara_CName+'</label>'+  
           '<div class="layui-input-block" style="white-space: nowrap;">'+ 
            '<div class="layui-input-inline">'+  
              checkedHtml+
            '</div>'+  
            '<div class="layui-inline layui-hide">'+  
             '<button class="layui-btn layui-btn-xs" id="'+formID+'_'+'btn_'+obj.BPara_ParaNo+'"><i class="iconfont">&#xe603;</i>&nbsp;'+btnTextHtml+'</button>'+
            '</div>'+
           '</div>'+  
         '</div> ' ;
         
		return html;
	};
	/** 创建单选框
	  *设计规则：数据类型代码|默认值|输入列表|下拉或展开(0下拉，1展开)
	 * @param {Object} obj 
	 * @param {Object} textLength   lable 长度
	 * @param {Object} type  类型
	 */
	Class.pt.createRadio = function(obj,textLength,defaultValue,formID){
		var me = this;
		//默认值(不选)
    	var defaultValue = me.getDefaultValue(obj);
    	var checkedHtml = me.createRadioHtml(formID,obj,defaultValue);
    	//输入列表不为空
    	var CL = me.getCheckBoxCL(obj);
    	if(CL){
    		//判断是展开方式还是下拉方式(0下拉，1展开）
			var showMode = me.IsCheckBoxShowMode(obj);
			if(showMode =='1'){
				checkedHtml = me.createSRadioHtml(formID,obj,defaultValue);
			}
			else {
				checkedHtml = me.createSComboHtml(formID,obj,defaultValue);
			}
			   
    	}
    	
    	var btnTextHtml = '采用出厂设置';
        if(formID=='Personality')btnTextHtml='采用默认值';
		var html = '<div class="layui-form-item">'+  
           '<label class="layui-form-label" style="width:'+textLength+'px">'+obj.BPara_CName+'</label>'+  
           '<div class="layui-input-block">'+
            '<div class="layui-input-inline">'+  
              checkedHtml+
            '</div>'+  
             '<div class="layui-inline layui-hide">'+  
             '<button class="layui-btn layui-btn-xs" id="'+formID+'_'+'btn_'+obj.BPara_ParaNo+'"><i class="iconfont">&#xe603;</i>&nbsp;'+btnTextHtml+'</button>'+
            '</div>'+
           '</div>'+  
         '</div> ' ;
		return html;
	};
    //创建颜色框
	Class.pt.createColor = function(obj,textLength,formID){
		var me = this;
		//默认值
		var defaultValue = me.getDefaultValue(obj) ;
        COLORLIST.push({
        	Id:formID+'_'+obj.BPara_ParaNo+'color-form',
        	ColorValue:defaultValue,
        	predefine: true,
//      	colors:['#ff4500','#1e90ff','rgba(255, 69, 0, 0.68)','rgb(255, 120, 0)'],
        	ColorID:formID+'_'+obj.BPara_ParaNo
        });

        var btnTextHtml = '采用出厂设置';
        if(formID=='Personality')btnTextHtml='采用默认值';
		var html ='<div class="layui-form-item">'+  
           '<label class="layui-form-label" style="width:'+textLength+'px">'+obj.BPara_CName+'</label>'+  
           '<div class="layui-input-block">'+  
            '<div class="layui-input-inline " style="width: 75px;">'+  
             '<input type="text" value="" placeholder="" class="layui-input" id="'+formID+'_'+obj.BPara_ParaNo+'" name="'+formID+'_'+obj.BPara_ParaNo+'" />'+  
            '</div>'+  
            '<div class="layui-inline" style="left: -6px;margin-top:-10px;">'+  
             '<div id="'+formID+'_'+obj.BPara_ParaNo+'color-form" style="height: 10px;"></div>'+  
            '</div>'+  
             '<div class="layui-inline layui-hide" style="padding-left: 47px;">'+  
             '<button class="layui-btn layui-btn-xs" id="'+formID+'_'+'btn_'+obj.BPara_ParaNo+'"><i class="iconfont">&#xe603;</i>&nbsp;'+btnTextHtml+'</button>'+
            '</div>'+
           '</div>'+  
         '</div> ' ;
		return html;
	};
	 /**
     * 计算字符长度
     *获得字符串实际长度，中文2，英文1
     *str要获得长度的字符串
     */
	Class.pt.getNum = function(str){
		if(!str) return;
		str = str.replace(/\s/ig,'');
	    var realLength = 0, len = str.length, charCode = -1;
	    for (var i = 0; i < len; i++) {
	      charCode = str.charCodeAt(i);
		  if (charCode >= 0 && charCode <= 128) 
		       realLength += 1;
		    else
		       realLength += 2;
	    }
	    return realLength/2;  
	};
	//核心入口
	paraform.render = function(options){
		var me = new Class(options);
		me.load = me.load;
		me.createControl = me.createControl;
		me.clearData = me.clearData;
		return me;
	};
	//渲染
	Class.pt.initListeners = function(list,formID){
		var  me = this;
		//浮点型F ,数字类型处理
        for(var i=0;i<NUMBERLIST.length;i++){
        	$("#"+formID+'_'+NUMBERLIST[i].Id).change(function(){
        		var max = Number($(this).attr('max'));
        		var min = Number($(this).attr('min'));
        		var decimal = $(this).attr('Decimal');
        		if(max && $(this).val()>max)$(this).val(max);
        		if(min && $(this).val()<min)$(this).val(min);
        		if(decimal)$(this).val(parseFloat($(this).val()).toFixed(decimal));
        	});
        }
        for(var i=0;i<COLORLIST.length;i++){
        	 //渲染颜色
			colorpicker.render({
			    elem: '#'+COLORLIST[i].Id,
				color: COLORLIST[i].ColorValue,
				predefine: false,
				size: 'xs',
//      	    colors:['#ff4500','#1e90ff','rgba(255, 69, 0, 0.68)','rgb(255, 120, 0)'],
				done: function(color){
					$(this.elem.split('color-form')[0]).val(color);
			    }
			});
			if(COLORLIST[i].ColorValue)$('#'+COLORLIST[i].ColorID).val(COLORLIST[i].ColorValue);
        }
  
    	//时间类型渲染
    	for(var i=0;i<DADELIST.length;i++){
		    laydate.render({
		      elem: '#'+DADELIST[i].Id,
		      type:DADELIST[i].type
		    });
    	}
//  	for(var i = 0;i<list.length;i++){
//  		//下拉列表返回值(下拉内容),第四个值|
//	    	var arr = list[i].BPara_ParaEditInfo.split('|');
//	    	if(arr[0] == 'CL'){
//	    		//下拉内容
//	    		var arr = arr[3].split('#') || [];
//	    		//取字符/后的规则
//      	    var rulearr = arr[arr.length-1].split("/");
//      	    //下拉多选
//      	    if(rulearr[2]=='0' || rulearr[3]=='1') {
//      	    	multiSelect.render('select',formID+'_'+list[i].BPara_ParaNo);
//      	    }
//	    	}
//  	}
    	multiSelect.render('select');
	};
	Class.pt.returnFloat = function(value){
		var value=Math.round(parseFloat(value)*100)/100;
		var xsd=value.toString().split(".");
		if(xsd.length==1){
		   value=value.toString()+".00";
		   return value;
		}
		if(xsd.length>1){
			if(xsd[1].length<2){
			    value=value.toString()+"0";
			}
		    return value;
		}
	};
	//参数加载
	Class.pt.load = function(URL,paraTypeCode,field,callback){
		var url  = uxutil.path.ROOT + URL;
		
		var fields='BPara_ParaNo,BPara_CName,BPara_TypeCode,BPara_TypeCode,BPara_TypeCode,BPara_TypeCode,BPara_ParaType,'+
            'BPara_ParaDesc,BPara_ParaEditInfo,BPara_SystemCode,BPara_ShortCode,BPara_BVisible,BPara_BVisible,BPara_IsUse,BPara_ParaValue,BPara_Id,BPara_DispOrder';
        if(field)fields=field;
		url += '&paraTypeCode='+paraTypeCode+'&fields='+fields;
        uxutil.server.ajax({
			url:url
		},function(data){
			callback(data);
		});
	};
	//暴露接口
	exports('paraform',paraform);
});