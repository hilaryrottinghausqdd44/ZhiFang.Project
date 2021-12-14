/**
	@name：列表内表单组件的创建与还原
	@author：liangyl
	@version 2021-08-09
 */
layui.extend({
	xmSelect: '../src/xm-select'
}).define(['uxutil','form','xmSelect'], function(exports){
	"use strict";
	
	var $ = layui.$,
		xmSelect = layui.xmSelect,
		form = layui.form,
		uxutil = layui.uxutil;
	
	//DB下拉列表数据查询服务
    var GET_BASIC_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc';
    //获取下拉数据集
    var GET_PARA_DIC_URL = uxutil.path.ROOT + '/ServerWCF/LabStarPreService.svc/LS_UDTO_GetParaDicData';
    //获取所有枚举
	var GET_TYPE_LIST_URL = '/ServerWCF/CommonService.svc/GetClassDic';

	//AL 保存后返回的值,临时存储
	var TEMP_AL_VALUE ="";
	var table_ind = null;
	//弹出窗体临时存储值 ---AL
	var AL_DATA = [];
	//弹出窗体临时存储值 ---BH
	var BH_DATA = [];
	//弹出窗体临时存储值 ---BC
	var BC_DATA = [];
	//弹出窗体临时存储值 ---BC
	var SH_DATA = [];
	//外部接口
	var ListForm = {
		SELECT_DATA_LIST :[],//返回的下拉框数据集
		//是否默认参数,true--默认参数
	    isDefault : function(ObjectID){
	    	var me = this;
	    	var isdefault =true;
			if(ObjectID && ObjectID.indexOf("_DefaultPara") == -1)isdefault=false;
			return isdefault;
	    },
	    getSelectList:function(ParaEditInfo){
	    	var me=this,
	    	    data = [],
	    	    tablename = me.getDbName(ParaEditInfo);
            for(var j=0;j<me.SELECT_DATA_LIST.length;j++){
            	if(me.SELECT_DATA_LIST[j].Code == tablename){
            		for(var i=0;i<me.SELECT_DATA_LIST[j].List.length;i++){
            			data.push({name:me.SELECT_DATA_LIST[j].List[i].DicDataName,value:me.SELECT_DATA_LIST[j].List[i].DicDataID});
            		}
            		break;
            	}
            }
            return data;
	    },
		initComRender : function(d,table_ind){
			var me = this;
			//表单赋值
			for(var i=0;i<d.length;i++){
				var id = d[i].BPara_ParaNo;
				var value = d[i].ParaValue +"";
				var title = d[i].BParaItem_BPara_CName;
				var ParaEditInfo = d[i].BPara_ParaEditInfo;
				var index = d[i].LAY_TABLE_INDEX;
				if(me.isDefault(table_ind.ObjectID)){ //选择默认参数取值
					value = d[i].BPara_ParaValue +"";
					id = 'Default_'+id;
					title = d[i].BPara_CName;
				}
				switch (me.getComType(ParaEditInfo)){
					case 'E':
					    if(value=='1')$("input[name='"+id+"']").prop("checked",true);
	                    form.render('checkbox');
						break;
					case 'CL':
			            me.initSelect(id,me.getClType(ParaEditInfo),me.getClList(ParaEditInfo),value,d[i],table_ind);
						break;
					case 'DB':
			            me.initSelect(id,me.getClType(ParaEditInfo),me.getSelectList(ParaEditInfo),value,d[i],table_ind);
						break;	
					case 'BH':
					    var CNameElemID = 'BH_Name'+id,
				            IdElemID = 'BH_Id'+id;
				        //还原
				        if(value){ //bH选择列表还原
				        	$("input[name='"+IdElemID+"']").val(value);
				        	$("input[name='"+CNameElemID+"']").val(me.setBHValue(value,me.getSelectList(ParaEditInfo)));
				        }
				        me.initBHListeners(IdElemID,CNameElemID,d[i],me.getSelectList(ParaEditInfo),title);

//				       
				 		break;	
					case 'SH':
						var CNameElemID = 'SH_Name'+id,
				            IdElemID = 'SH_Id'+id;
				        var sh_data = me.getSelectList(ParaEditInfo);
				        var cllist = me.getSHType(ParaEditInfo); //sh下拉数据
				        //还原
				        if(value ){ //SH选择列表不包含下拉框时，还原
				        	$("input[name='"+IdElemID+"']").val(value);
				        	$("input[name='"+CNameElemID+"']").val(me.setSHValue(value,sh_data,cllist));
				        }
				        me.initSHListeners(IdElemID,CNameElemID,d[i],sh_data,cllist,title);
						break;	
					default: //默认为文本框C
					    $("input[name='"+id+"']").val(value);
					    me.initTableListeners(id,d[i],me.getComType(d[i].BPara_ParaEditInfo),table_ind);
						break; 
				} 
				form.render();
			}
			//下拉框 -- icon 前存在icon 则点击该icon 等同于点击input
		    $("td .layui-input+.layui-icon").on('click', function () {
		        if (!$(this).hasClass("myDate")) {
		            $(this).prev('td .layui-input')[0].click();
		            return false;//不加的话 不能弹出
		        }
		    });
		},
		//获取组件类型
	    getComType : function(ParaEditInfo){
			var  me = this,
			     type = 'C';
	        if(!ParaEditInfo){
	        	type = 'C';
	        } else{
	        	var valType = ParaEditInfo.toUpperCase();
	        	type = valType;
	    		//如果存在多个值，截取第一个|前的内容
	    		if(valType.length>1){
	    			var str = valType.split('|');
	    			type = str[0];  //取第一个
	    		}
	        }
	    	return type;
		},
     	//创建组件输入框
	    createEditInfo : function(d,type,com_data,ParaEditInfo){
			var  me = this;
	    	var HTML = "";
	    	me.SELECT_DATA_LIST = com_data; 
	    	var ParaNo  = d.BPara_ParaNo;
			if(type)ParaNo = 'Default_'+ParaNo;
	        switch (me.getComType(ParaEditInfo)){
				case 'E':
	    			HTML = me.createCheckbox(d,ParaNo);
					break;  
				case 'CL':;
	    		    HTML = me.createCL(d,ParaNo);
					break;
				case 'DB':
	    		    HTML = me.createDB(d,ParaNo)
	                break;
				case 'I':
	                HTML = me.createNum(d,ParaNo);
					break;
				case 'AL':
	                HTML = me.createAL(d,ParaNo);
					break;
				case 'SH':
	                HTML = me.createSH(d,ParaNo);
	                break;
	            case 'BC':
	                HTML = me.createBC(d,ParaNo);
					break;
				case 'BH':
	                HTML = me.createBH(d,ParaNo);
					break;
				default: //默认为文本框C
				    HTML = me.createTextBox(d,ParaNo);
					break;
			}  
	    	return HTML;
		},
	   //创建文本框   设计规则：数据类型|默认值  
	    createTextBox : function(d,ParaNo){
			var  me = this;
	    	var HTML = '<input type="text" name="'+ParaNo+'" autocomplete="off" class="layui-input" style="position: absolute;left:0; top: 0;  padding: 0px 10px 0px; margin:-5px 0px 0px 0px;height:28px; ">'
	    	return HTML;
		},
		//创建E  设计规则：数据类型|默认值   是和否  （1,0)
		createCheckbox : function(d,ParaNo){
			var  me = this;
			var HTML = '<input type="checkbox" name="'+ParaNo+'" title="" lay-skin="primary" lay-filter="checkbox2" >';
	    	return HTML;
		},
	    //创建数字框  设计规则：数据类型|默认值   是和否  （1,0)
		createNum : function(d,ParaNo){
			var  me = this;
	    	var HTML = '<input type="number" autocomplete="off" class="layui-input" id="'+ParaNo+'" name="'+ParaNo+'" style="position: absolute;left:0; top: 0;  padding: 0px 10px 0px; margin:-5px 0px 0px 0px;height:28px; ">'; 
	    	return HTML;
		},
		//创建下拉框  设计规则 :数据类型|默认值|长度|下拉列表|多选(1)
		createCL : function(d,ParaNo){
			var  me = this;
	    	var HTML =
	    	    '<div id="'+ParaNo+'"class="layui-form layui-form-pane xm-select layui-table-edit2" lay-filter="'+ParaNo+'" style="position: absolute;left:0; top: 0; width:100%;height:28px;margin:-5px 0px 0px 0px;padding:0px;"></div>';
	    	return HTML;
		},
		//创建弹出列表  设计规则:数据类型|模式   （模式1(0)-核收条件选择,模式2(1)-病人信息分组规则,模式3(2)-条码打印字段配置）
		createAL : function(d,ParaNo){
			var  me = this;
	    	var HTML = '<input type="text" name="'+ParaNo+'" autocomplete="off" readonly class="layui-input" style="position: absolute;left:0; top: 0;  padding: 0px 10px 0px; margin:-5px 0px 0px 0px;height:28px; ">'
	    	return HTML;
		},
		//创建根据条码号获取对应就诊类型选择  设计规则:数据类型|就诊类型(取字典数据,可配置)
		createBC : function(d,ParaNo){
			var  me = this;
	    	var HTML = '<input type="text" name="'+ParaNo+'" autocomplete="off" readonly class="layui-input" style="position: absolute;left:0; top: 0;  padding: 0px 10px 0px; margin:-5px 0px 0px 0px;height:28px; ">'
	    	return HTML;
		},
		//创建根据条码号获取对应就诊类型选择  设计规则:数据类型|叫号字典(取字典数据,可配置)
		createBH : function(d,ParaNo){
			var  me = this;
			var HTML = '<input type="text"  name="BH_Id'+ParaNo+'"  class="layui-input  layui-hide" />'+
		        '<input type="text"  name="BH_Name'+ParaNo+'" readonly autocomplete="off" class="layui-input" style="position: absolute;left:0; top: 0;  padding: 0px 10px 0px; margin:-5px 0px 0px 0px;height:28px; "/>';
	    	return HTML;
		},
		//弹出SH 设计规则：SH  1:显示选定  2:显示未选定  注解：显示选定和未选定的含义解释如下，拼接SQL语句时如果是显示选定则为字段IN下拉框选择的数据，如果是未选定则为字段INOT IN下拉框选择的数据
		createSH : function(d,ParaNo){
			var  me = this;
			var HTML = '<input type="text"  name="SH_Id'+ParaNo+'"  class="layui-input  layui-hide" />'+
		        '<input type="text"  name="SH_Name'+ParaNo+'" readonly autocomplete="off" class="layui-input" style="position: absolute;left:0; top: 0;  padding: 0px 10px 0px; margin:-5px 0px 0px 0px;height:28px; "/>';
	    	return HTML;
		},
		//创建下拉框（数据从数据库中取） 设计规则 :DB||LBSamplingGroup|1   数据类型|默认值|查询表|多選(1),單選(0)
		createDB : function(d,ParaNo){
			var  me = this;
			var HTML =
	    	    '<div id="'+ParaNo+'"class="layui-form layui-form-pane xm-select layui-table-edit2" lay-filter="'+ParaNo+'" style="position: absolute;left:0; top: 0; width:100%;height:28px;margin:-5px 0px 0px 0px;padding:0px;"></div>';
	    	return HTML;
		},
		//解析下拉列表内容
		getClList : function(ParaEditInfo){
		    var list = [],list2 = [];
	        if(ParaEditInfo){
				//下拉列表返回值(下拉内容),第四个值|
		    	var arr = ParaEditInfo.split('|');
		    	var strval = "";
		    	if(arr.length>=3)strval = arr[3];//第四个数组
		    	//下拉内容
		    	list = strval.split('#') || [];
	    	}
	        
	        for(var i=0;i<list.length;i++){
	        	var str = "";
	        	str = list[i].split('&') || [];
	        	list2.push({
	        		value:str[0],
	        		name:str[1]
	        	});
	        }
	        return list2;
	    },
	    //解析下拉框是是单选还是多选
	    getClType : function(ParaEditInfo){
	    	var me=this,
	    	    type = 'radio';
		    var comtype = me.getComType(ParaEditInfo);
	        if(ParaEditInfo){
				//下拉列表返回值(下拉内容),第四个值|
		    	var arr = ParaEditInfo.split('|');
		    	var strval = "";
		    	if(arr.length>=4){
		    		if(comtype == 'CL')strval = arr[4]+'';//第5个数组
		    		else
		    		    strval = arr[3]+'';//第4个数组
		    	}
		    	if(strval == '1')type="";//多选
	    	}
	        return type;
	    },
	    //初始化下拉数据组件
	    initSelect : function(id,type,data,ParaValue,d,table_ind){
	    	var me = this;
	    	var obj ={
				el: '#'+id,
				language: 'zn',
				data: data,
			    size: 'mini'
			};
			if(type =='radio'){
				obj.radio=true;
				obj.clickClose= true;
				obj.model={
					icon: 'hidden',
					label: {
						type: 'text'
					}
				}
			}
			obj.on = function(data2){
				//arr:  当前多选已选中的数据
				var arr = data2.arr;
				//这里是当选择一个下拉选项的时候 把选择的值赋值给表格的当前行的缓存数据 否则提交到后台的时候下拉框的值是空的
			  	var filter = $(table_ind.config.elem).attr("lay-filter");
			  	var tableCache = table_ind.table.cache[filter]; 
			  	var list = [];
			  	for(var i=0;i<arr.length;i++){
			  		list.push(arr[i].value);
			  	}
			  	var id = list.join(',') || '';
		  		//这里是当选择一个下拉选项的时候 把选择的值赋值给表格的当前行的缓存数据 否则提交到后台的时候下拉框的值是空的
			  	me.updateItem(table_ind,d.LAY_TABLE_INDEX,id);
			};
			var demo1 = xmSelect.render(obj);
			demo1.setValue(me.setComValue(ParaValue,data));
	    },
		setComValue : function (ParaValue,list){
	    	var arr = [],data=[];
		    if(ParaValue)data = ParaValue.split(',');
	        for(var i=0;i<data.length;i++){
				for(var j=0;j<list.length;j++){
					if(data[i]==list[j].value){
					   arr.push(list[j]);
					   break;
					}
				}
			}
	        return arr;
	    },
		//解析DB|SH下拉列表查询服务表名
		getDbName : function(ParaEditInfo){
			var me = this;
		    var str = "";
	        if(ParaEditInfo){
				//下拉列表返回值(下拉内容),第四个值|
		    	var arr = ParaEditInfo.split('|');
		    	if(arr.length>=2)str = arr[2];//第2个数组
	        }
	        return str;
	   },
		//修改数据后更新缓存
        updateItem : function(table_ind,dataindex,value){
	    	var me = this;
	    	var filter = $(table_ind.config.elem).attr("lay-filter");
		  	var filed = 'ParaValue';
			if(me.isDefault(table_ind.ObjectID))filed = 'ParaValue2';
	    	layui.$.extend(table_ind.table.cache[filter][dataindex],{[filed] : value});
        },
        initTableListeners : function(IdElemID,d,type,table_ind){
	    	var me = this;
	    	if(type == 'C' || type == 'I'){
	    		$("input[name='"+IdElemID+"']").bind('input propertychange', function() {
			      	//这里是当选择一个下拉选项的时候 把选择的值赋值给表格的当前行的缓存数据 否则提交到后台的时候下拉框的值是空的
				  	me.updateItem(table_ind,d.LAY_TABLE_INDEX,$("input[name='"+IdElemID+"']").val());
			    });
	    	}
	    	if(type == 'AL1' || type == 'AL2' || type == 'AL3'){ //弹出对应数据集
	    		$("input[name='"+IdElemID+"']").click(function(){
	    			AL_DATA = $("input[name='"+IdElemID+"']").val();
	    			//模式1 核收条件选择  模式2：病人信息分组规则  模式3： 条码打印
	                me.openAL(type,d.BPara_CName,IdElemID,d,table_ind);
				});
	    	}
	    	if(type == 'BC'){ //根据条码号获取对应就诊类型
	    		$("input[name='"+IdElemID+"']").click(function(){
	    			var bc_data = me.getSelectList(d.BPara_ParaEditInfo);
	    			var value = $("input[name='"+IdElemID+"']").val() || '';
   	                BC_DATA= {id:value,data:bc_data};
	    			me.openBC(IdElemID,d);
				});
	    	}
	    	
	    },
	    initSHListeners : function(IdElemID,CNameElemID,d,sh_data,cllist,title){
	    	var me = this;
    		$("input[name='"+CNameElemID+"']").click(function(){
    			var obj ={id:$('input[name="'+IdElemID+'"]').val(),name:$('input[name="'+CNameElemID+'"]').val(),data:sh_data,cllist:cllist};
    			SH_DATA = obj;
                me.openSH(IdElemID,CNameElemID,title,d.LAY_TABLE_INDEX);
			});
	    },
	    initBHListeners : function(IdElemID,CNameElemID,d,bh_data,title){
	    	var me = this;
	        $("input[name='"+CNameElemID+"']").click(function(){
    			var value = $("input[name='"+IdElemID+"']").val() || '';
   	            BH_DATA = {id:value,data:bh_data};
    			me.openBH(CNameElemID,IdElemID,title,d.LAY_TABLE_INDEX);
			});
	    },
	    openAL : function(model,title,IdElemID,d,table_ind){
			var me = this;
			//默认多选
			var choice = 'checkbox';
			//核收条件具体参数名称
			var paraName ="";
			var BPara_ParaEditInfo = d.BPara_ParaEditInfo;
			if(model=='AL1' || model=='AL2'){
				var arr = BPara_ParaEditInfo.split('|');
				//单选 是AL1|0   ------模式1 才有单选，模式2只有多选
				if(arr.length>2 && arr[2]=='0' && model=='AL1')choice='radio';
				//核收条件名
				if(arr.length>1 && arr[1])paraName=arr[1];
			}
			layer.open({
	            type: 2,
	            area: ['95%', '95%'],
	            fixed: false,
	            maxmin: false,
	            title:title,
	            content: 'obarcodefields.html?model='+model+'&id='+IdElemID+'&num='+d.LAY_TABLE_INDEX+'&choice='+choice+'&paraName='+paraName
	        });
		},
		openBC: function(IdElemID,d){
			var me = this;
			layer.open({
	            type: 2,
	            area: ['90%', '90%'],
	            fixed: false,
	            maxmin: false,
	            title:d.BPara_CName,
	            content: 'barcode.html?id='+IdElemID+'&num='+d.LAY_TABLE_INDEX
	        });
		},
	    openBH: function(CNameElemID,IdElemID,title,num){ //叫号系统数据更新     
			var me = this;
			layer.open({
	            type: 2,
	            area: ['500px', '90%'],
	            fixed: false,
	            maxmin: false,
	            title:title,
	            content: 'callsys.html?id='+IdElemID+'&name='+CNameElemID+'&num='+num+'&t=' + new Date().getTime()
	        });
		},
		openSH : function(IdElemID,ElemName,title,num){
			var me = this;	   
			layer.open({
	            type: 2,
	            area: ['470px', '90%'],
	            fixed: false,
	            maxmin: false,
	            title:title,
	            content: 'sh.html?t=' + new Date().getTime()+'&id='+IdElemID+'&name='+ElemName+'&num='+num
	        });
		},
		getSHValue : function(){
			return SH_DATA;
		},
		getBHValue : function(){
			return BH_DATA
		},
		getBCValue : function(IdElemID,data){
			return BC_DATA;
		},
		getAlValue : function(){
			return AL_DATA;
		},
		//BH还原,选择列表名称匹配还原匹配name
		setBHValue: function(value,data){
			var me = this,
			    str_value ="";
			var id_data =  value.split(',');
			//还原值处理
	    	var sp_arr =[];
	    	for(var i=0;i<id_data.length;i++){
				var str1 = id_data[i].split('-');
				for(var j=0;j<data.length;j++){
					if(data[j].value == str1[0]){
						sp_arr.push(data[j].name+'-'+str1[1]);
						data.splice(j,1);
						break;
					}
				}
			}
			return sp_arr.join(',');
		},
        //dic下拉数据集，根据id 分隔匹配name
        getNameByID : function(ids,data){
        	var me = this;
        	var names = [],str_name=ids;
        	for(var i=0;i<data.length;i++){
				for(var j=0;j<ids.length;j++){
					if(ids[j] == data[i].value){
						names.push(data[i].name);
						break;
					}
				}
			}
    	    if(names.length>0)str_name = names.join(',');
    	    return str_name;
        },
		//还原sh值，不带下拉框
		getSHName : function(value,data){
			var me = this,
			    pre = "",str ="" ,str_name="",names=[];
			var arr = value.split('|');
    	    if(arr.length>0){
    	    	pre = arr[0]; //是否选定值
    	    	str = arr[1];//id
    	    }
    	    if(str.length ==0)return str_name;
    	    //根据id 匹配name
    	    var ids = str.split(',');
    	    str_name = pre+'|'+me.getNameByID(ids,data);
    	    return str_name;
		},
		//还原sh值，带下拉框
		getSHComName : function(value,data,cllist){
			var me = this,
			    str_name="",names=[];
			//分隔
    	    var arr = value.split(',');
    		for(var i = 0;i<arr.length;i++){
    			var arr2 = arr[i].split('&');
    			var str1 = me.getNameSHbyId(arr2[0],data);
				var str2 = me.getSHClist(arr2[1],cllist);
				names.push(str1+'&'+str2);
    		}
    	    if(names.length>0)str_name=names.join(',');
    	    return str_name;
		},
		//sh下拉数据返回name
		getSHClist:function(value,cllist){
			var me = this,
			    clname = "";
			var arr = cllist.split('#');
			for(var i = 0;i<arr.length;i++){
				var arr2 = arr[i].split('&');
				if(value == arr2[0]){
					clname = arr2[1];
				    break; 
				}
			}
			return clname;
		},
		//sh下拉数据返回name
		getNameSHbyId:function(value,data){
			var me = this,
			    name = value;
			for(var i = 0;i<data.length;i++){
				if(value == data[i].value){
					name = data[i].name;
				    break; 
				}
			}
			return name;
		},
		//获取sh 类型,是否包含下拉框
		getSHType : function(ParaEditInfo){
			var cllist = "";
			ParaEditInfo =  ParaEditInfo.split('|');
			if(ParaEditInfo.length>3)cllist = ParaEditInfo[3];
			return cllist;
		},
		//SH还原,匹配name
		setSHValue: function(value,data,cllist){
			var me = this;
			var str_value =value;
			if(cllist){ //选择列表带下拉还原
				str_value = me.getSHComName(value,data,cllist);
			}else{ //选择列表不带下拉还原
				str_value = me.getSHName(value,data);
			}
			return str_value;
		},
		//还原CL|DB name 显示
		setDisplayName : function (ParaValue,data){
	    	var value ="",names =[];
		    for(var i in data){
		    	var arr = ParaValue.split(',');
		    	if(arr.length==1)arr = [ParaValue];
		    	for(var j in arr){
		    		if(arr[j] == data[i].value){
		    			names.push(data[i].name);
		        		break;
		        	}
		    	}
	        }
		    if(names.length>0)value = names.join(',');
		    return value;
	    }
	};
	window.getSHValue = ListForm.getSHValue;
    window.getBHValue = ListForm.getBHValue;
	window.getBCValue = ListForm.getBCValue;
	window.getAlValue = ListForm.getAlValue;
	//暴露接口
	exports('ListForm',ListForm);
});