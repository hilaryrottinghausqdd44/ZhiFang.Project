/**
 * 高级查询配置--控制文件类
 * 表单控件生成的位置(x,y)显示
 * 每个一个组件大小为160
 * 每一行为四个控件,每一行控件间隔20
 * 每一行第一个控件x轴为10,每一行第二个控件x轴为190,每一行第三个控件x轴为380,每一行第四个控件x轴为560
 * 第一行第一个控件y轴为10,其他的每一行的y轴间隔32
 */
Ext.define("ZhiFang.controller.AdvancedSearchController",{
	extend:'Ext.app.Controller',
	views:['FormItemParamsView','FormParamsView','FormView','FormComponentsTreeView'],//视图
	stores:['FormItemParamsStore','FormViewStore','FormComponentsTreeStore','ZhiFang.store.FormItemComboBoxStore'],//数据集合
	models:['FormItemParamsModel','ZhiFang.model.FormItemComboBoxModel'],//模型
    
	init:function(){
		var application ="";//可执行代码
		var resouce = "";//可还原代码
		var GUID = GetGUID();//GUID码
		var formId = "LabStar_AdvancedSearch"+GUID;//列表唯一编码
		var background = false;//是否有格子背景
/*------------------------------------------------------------*/
		//控制代码
		this.control({
			/*----------添加图片、纯文本、按钮事件----------*/
			'button[id=image]':{//添加图片
				click:function(o){
					var id = GetGUID();//GUID码
					var form = Ext.getCmp('FormView');
					var image = CreateImage(id);
					form.add(image);
					InsertToParamsList(image);
				}
			},
			'button[id=label]':{//添加纯文本
				click:function(o){
					var id = GetGUID();//GUID码
					var form = Ext.getCmp('FormView');
					var label = CreateLabel(id);
					form.add(label);
					InsertToParamsList(label);
				}
			},
			'button[id=button]':{//添加按钮
				click:function(o){
					var id = GetGUID();//GUID码
					var form = Ext.getCmp('FormView');
					var button = CreateButton(id);
					form.add(button);
					InsertToParamsList(button);
				}
			},
			'formParamsView button[id=ColumnOk]':{//展示列勾选
				click:function(o){
					ColumnInfoList();//填充数据项属性
					FormViewRefresh();//浏览效果
				}
			},
            'formParamsView button[id=btnSelect]':{
                click:function(o){
                    //数据对象:下拉列表的服务地址
                    var  myServerUrl=Ext.getCmp("FormParamsView_FormModel").getValue();
                    var  myUrl=Ext.getCmp("ComboBoxObjectUrl").getValue();
                    if((myServerUrl==""||myServerUrl==null)||(myUrl==""||myUrl==null)){
                     Ext.Msg.alert("请填写配置好服务地址再操作!");
                     return;
                    }
                    var combo = Ext.getCmp('ObjectListCombobox');
                    var myStore=new Ext.data.Store({
                        fields:['CName','Object','KJM','Explanation'],
                        proxy:{
                            type:'ajax',
                            url:myUrl,
                            reader:{type:'json',root:'list.list'}
                        }
                    })
                    combo.store=myStore; 
                    combo.store.load();
                }
            },
            //数据对象选择
            'formParamsView combobox[id=ObjectListCombobox]':{
                change:function(combo ,newValue,oldValue,eOpts ){
                //CreateFormModelGridStore(combo,records);
                },
                select:function(combo,records,eOpts){
                   CreateFormModelGridStore(combo,records);
                }
            },
            
             //右边视图的列表视图里的下拉列表数据服务
            'formParamsView gridpanel combobox[id=DataUrlComboBox]':{
//                beforerender:function(combo ,eOpts ){
//                    alert(combo);
//                DataUrlComboBoxStore(combo,eOpts);
//                //LastDataUrlComboBoxStore(combo,newValue);
//                },
//               
//                select:function(combo,records,eOpts){
//                   alert(combo);
//                DataUrlComboBoxStore(combo,eOpts);
//                }
            },
            //左下边视图的列表视图里的下拉列表数据服务
            'formParamsView combobox[id=LastDataUrlComboBox]':{
//                beforerender:function(combo ,eOpts ){
//                     alert(combo);
//                LastDataUrlComboBoxStore(combo,eOpts);
//                },
//                select:function(combo,records,eOpts){
//                    alert(combo);
//                LastDataUrlComboBoxStore(combo,newValue);
//                }
            }, 
			'button[id=show]':{//展示效果
				click:function(o){
					FormViewRefresh();
				}
			},
            
			'formView tool[id=toggle]':{//设置背景图
				click:function(event,toolEl,panel){
					var form = Ext.getCmp('FormView');
					if(background){
						Ext.getCmp('FormView').setBodyStyle('background:#fff;');
					}else{
						Ext.getCmp('FormView').setBodyStyle('background-image:url(../css/images/bk.jpg)');
					}
					background = !background;
					SetItems(form);//设置视图属性
				}
			},
			'button[id=save]':{
				click:function(o){
		            var mygrid=Ext.getCmp("FormItemParamsView");
		            var store=mygrid.getStore(); 
		            var lastValues=[];
                    lastValues.push("list:");
		            store.each(function(model){
		            lastValues.push(model.data);
		            });
		            alert(Ext.encode(lastValues));  
				}
			}
		});
/*--------------------------创建组件-----------------------*/

		//创建一个纯文本组件
		function CreateLabel(id){
			var me = {
				xtype:'label',text:'纯文本',Type:'Label',
				id:id,x:10,y:10,width:160,height:22,
				draggable:true,
				resizable: {
		            dynamic: true,
		            pinned: true,
		            handles: 'all'
				}
			};
			return me;
		}
		//创建一个按钮组件
		function CreateButton(id){
			var me = {
				xtype:'button',text:'按钮',Type:'Button',
				id:id,x:10,y:10,width:80,height:22,
				draggable:true,
				resizable: {
		            dynamic: true,
		            pinned: true,
		            handles: 'all'
				}
			};
			return me;
		}
		//属性类表中新增记录
		function InsertToParamsList(ob){
			var list = Ext.getCmp('FormItemParamsView');
			var store = list.store;
			var rec = Ext.ModelMgr.create({
				CName : ob.text,
				EName : '',
				Type: ob.Type,
				X: ob.x,
				Y: ob.y,
				Width: ob.width,
				LabelWidth: 0,
				Height: ob.height,
				IsHidden:false,
				Url:ob.src,
                Id:ob.id,
                DataUrl:ob.DataUrl
			}, 'ZhiFang.model.FormItemParamsModel');
			store.add(rec);
		}
/*--------------------------效果浏览-----------------------*/
		//展示列表(center)
		function FormViewRefresh(){
			var form = Ext.getCmp('FormView');
			SetHead(form);//设置标题属性
			SetItems(form);//设置视图属性
			
		}
		//展示表单标题属性设置
		function SetHead(form){
//			var HeadParams = Ext.getCmp('FormParamsView_HeadParams').getForm().getFieldValues();
//			//标题名称属性<a style="font-family:隶书;font-size:24px;">
//			var style = "";
//			if(HeadParams['Size'] != 0){
//				style += "font-size:" + HeadParams['Size'] + "px;";
//			}
//			if(HeadParams['Color'] != ""){
//				style += "color:" + HeadParams['Color'] + ";";
//			}
//			if(HeadParams['Style'] != ""){
//				style += "font-family:" + HeadParams['Style'] + ";";
//			}
//			var title = "";
//			//标题名称
//			title = "<a style='" + style + "'>" + HeadParams['Title'] + "</a>";
//			form.setTitle(title);
		}
		//表单内部组件设置
		function SetItems(form){
			SetItemsC(form);//不带外框
		}
		//每个组件都不带外框
		function SetItemsC(form){
			var list = Ext.getCmp('FormItemParamsView');
			var listItems = list.store.data.items;
			form.removeAll();
			for(var i in listItems){
				var ob = GetComponent(listItems[i]);
				if(ob != null){
					form.add(ob);
				}
			}
		}
		//每个组件都带外框
		function SetItemsW(form){
			var list = Ext.getCmp('FormItemParamsView');
			var listItems = list.store.data.items;
			form.removeAll();
			for(var i in listItems){
				var ob = GetWin(listItems[i]);
				if(ob != null){
					form.add(ob);
				}
			}
		}
		//获取带边框组件
		function GetWin(ob){
			var item = GetComponent(ob);
			item.x = 0;
			item.y = 0;
			item.draggable = false;
			var me = ('Ext.panel.Panel',{
				width:ob.get('Width'),
				height:ob.get('Height'),
				x:ob.get('X'),
				y:ob.get('Y'),
				hidden:ob.get('IsHidden'),
				items:item
			});
			return me;
		}
		//获取组件
		function GetComponent(ob){
			var me;
			var Type = ob.get('Type');
			if(Type === "ComboBox"){//下拉框
				me = GetComboBox(ob);
			}else if(Type === "TextField"){//文本框
				me = GetTextField(ob);
			}else if(Type === "TextAreaField"){//文本域
				me = GetTextAreaField(ob);
			}else if(Type === "NumberField"){//数字框
				me = GetNumberField(ob);
			}else if(Type === "DateField"){//日期框
				me = GetDateField(ob);
			}else if(Type === "TimeField"){//时间框
				me = GetTimeField(ob);
			}else if(Type === "Checkbox"){//复选框
				me = GetCheckbox(ob);
			}else if(Type === "Radio"){//单选框
				me = GetRadio(ob);
			}else if(Type === "Label"){//纯文本
				me = GetLabel(ob);
			}else if(Type === "Button"){//按钮
				me = GetButton(ob);
			}else{
				return null;
			}
            
			//me.id = ob.get('Id');
			me.height = ob.get('Height');
			me.hidden = ob.get('IsHidden');
			me.draggable = true;//可拖动
			me.x = ob.get('X');
			me.y = ob.get('Y');
			me.width = ob.get('Width');
			me.height = ob.get('Height');
			me.resizable = {
	            dynamic: false,//是否动态
	            pinned: false,
	            handles: 'all'
			};
 
            me.DataUrl= ob.get("DataUrl");
			me.listeners = GetMouseListeners(me);//组件拖动监听
			me.resizable.listeners = GetResizableListeners(me);//组件大小变化监听
			return me;
		}
		//获取组件拖动监听
		function GetMouseListeners(component){
			var mCom = component;
			mCom.xValue = 0;
			mCom.yValue = 0;
			var me = {
				mousedown:{
		            element: 'el',
		            fn: function(e,t,s){
						mCom.xValue = e.getX();
						mCom.yValue = e.getY();
					}
		        },
				mouseup:{
		            element: 'el',
		            fn: function(e,t,s){
						xValue2 = e.getX();
						yValue2 = e.getY();
						var x = xValue2 - mCom.xValue;
						var y = yValue2 - mCom.yValue;
						mCom.x += x;
						mCom.y += y;
						SetListValues(mCom.id,mCom.x,mCom.y,null,null);
					}
		        }
			};
			return me;
		}
		//获取组件大小变化监听
		function GetResizableListeners(component){
			var mCom = component;
			var me = {
				beforeresize:{
					element: 'el',
		            fn: function(a,width,height,e,eOpts){
					}
				},
				resize:{
		        	element: 'el',
		            fn: function(a,width,height,e,eOpts){
						SetListValues(mCom.id,null,null,width,height);
					}
		        }
			}
			return me;
		}
		function SetListValues(id,x,y,width,height){
			var list = Ext.getCmp('FormItemParamsView');
			var store = list.store;
			var index = store.findExact('Id',id);//是否存在这条记录
			if(index != -1){
				var item = store.getAt(index);
				if(x != null && y != null){
					item.set('X',x);
					item.set('Y',y);
				}
				if(width != null && height != null){
					item.set('Width',width);
					item.set('Height',height);
				}
				item.commit();
			}
		}
		
        //获取下拉框
		function GetComboBox(ob){
           var DataUrl=ob.get("DataUrl"),
           myComboboxRoot='list';
			var me = {
				xtype:'combobox',
				name:ob.get('EName'),
				fieldLabel:ob.get('CName'),
				mode:'local',
				editable:false,
				displayField:'text',
				valueField:'value',
				//value:1,
				store:getMyComboboxStore(DataUrl,myComboboxRoot),
				labelWidth:ob.get('LabelWidth')
			};
			return me;
		}
   //获得生成下拉列表的数据源store
   function getMyComboboxStore(DataUrl,myComboboxRoot){
    var me = this;
    var myComboboxStore=null;
    var url=DataUrl;
    if(url==null||url==""){
    Ext.Msg.alert('提示','需要绑定下拉列表的数据服务地址！');
    return null; 
    }
        Ext.Ajax.request({
            async:false,//非异步
            url:DataUrl,
            method:'POST',
            timeout:5000,
            success:function(response,opts){
            var result = Ext.JSON.decode(response.responseText);
            if(result.success){
            myComboboxStore=Ext.create('Ext.data.Store', {  
            fields:['value','text'], //me.DeptField,//实现数据项适配的功能,需要调用时传入DeptpId,DeptpName
            data:result,
            proxy: {
            type: 'memory',
            reader: {
            type: 'json',
            root: myComboboxRoot  //myComboboxRoot为后台返回的数据源的root
            }
        }
    });
            }else{
                    Ext.Msg.alert('提示','获取信息失败！');
                }
            },
            failure : function(response,options){ 
                Ext.Msg.alert('提示','获取信息请求失败！');
            }
        });
    return myComboboxStore;
   }
		//获取文本框
		function GetTextField(ob){
			var me ={
				xtype:'textfield',
                //itemId:Ext.id(),
                value:'',
				name:ob.get('EName'),
				fieldLabel:ob.get('CName'),//'<font size=2 color=red>'+ob.get('CName')+'</font>',
				labelWidth:ob.get('LabelWidth')
			};
			return me;
		}
		//获取文本域
		function GetTextAreaField(ob){
			var me ={
				xtype:'textareafield',
                //itemId:Ext.id(),
                value:'',
				name:ob.get('EName'),
				fieldLabel:ob.get('CName'),
				labelWidth:ob.get('LabelWidth')
			};
			return me;
		}
		//获取数字框
		function GetNumberField(ob){
			var me = {
				xtype:'numberfield',
				itemId:ob.get('EName'),
				name:ob.get('EName'),
				fieldLabel:ob.get('CName'),
				labelWidth:ob.get('LabelWidth')
			};
			return me;
		}
		//获取日期框
		function GetDateField(ob){
			var me = {
				xtype:'datetimefield',
                itemId:Ext.id(),
				readOnly: false,
                selectOnFocus: true,
				DateFormat: 'Y-m-d',
				name:ob.get('EName'),
				LabField:ob.get('CName'),
				LabFieldWidth:ob.get('LabelWidth')
                
			};
			return me;
		}
		//获取时间框
		function GetTimeField(ob){
			var me = {
				xtype:'datetimefield',
                itemId:Ext.id(),
				readOnly: false,
                selectOnFocus: true,
                TimeFormat: 'H:i',
                name:ob.get('EName'),
                LabField:ob.get('CName'),
                LabFieldWidth:ob.get('LabelWidth')
			};
			return me;
		}
		//获取复选框
		function GetCheckbox(ob){
			var me = {
				xtype:'checkboxfield',
                itemId:Ext.id(),
                name:ob.get('EName'),
				boxLabel:ob.get('CName'),
				labelWidth:ob.get('LabelWidth')
			};
			return me;
		}
		//获取单选框
		function GetRadio(ob){
			var me = {
				xtype:'radiofield',
                itemId:Ext.id(),
                name:ob.get('EName'),
				boxLabel:ob.get('CName'),
				labelWidth:ob.get('LabelWidth')
			};
			return me;
		}
		//获取纯文本
		function GetLabel(ob){
			var me = {
				xtype:'label',
                itemId:Ext.id(),
                name:ob.get('EName'),
                text:ob.get('CName')
			};
			return me;
		}
		//获取按钮
		function GetButton(ob){
			var me = {
				xtype:'button', 
                itemId:Ext.id(),
                name:ob.get('EName'),
				text:ob.get('CName')
			};
			return me;
		}
/*------------------------------------------------------------*/
		//生成列属性勾选模块
		function SelectColumn(url){
			var FormModelGrid = Ext.getCmp('FormParamsView_FormModelGrid');
			FormModelGrid.store.proxy.url =url; 
			FormModelGrid.store.load();
            //alert("test");
		}
        
		//刷新列信息列表(south)
		function ColumnInfoList(){
			var list = Ext.getCmp('FormItemParamsView');
			var ColumnParams = Ext.getCmp('FormParamsView_FormModelGrid');//FormParamsView_FormModelGrid
			var data = ColumnParams.getSelectionModel().getSelection();
			if(data.length == 0){
				Ext.Msg.alert('提示','您至少要选择一条数据！');
			}else{
				var store = list.store;
                var length=data.length;
                var x=10,y=5;//默认
				store.removeAll();//删除原先的列

                var i=0
                for(i=0;i<=length-1;i++){

                var dd= i%4;//取整
                //处理y轴位置,每行生成四个控件,当dd= i%4为整数时换下一行,y轴值需要加32
                if(dd==0&&i!=0){
                    y=y+32;
                    }
                    //每一行为四个控件,处理x轴位置
                    if(dd==1)//
                    {
                    x=210;//每一行第二个控件x轴为210,
                    }else if(dd==2){
                    x=400;//每一行第三个控件x轴为400
                    }else if(dd==3){
                    x=590;
                    }else{
                    x=10;//每一行第一个控件x轴为10
                    }

                    var rec = Ext.ModelMgr.create({
                        Id:GetGUID(),
                        CName: data[i].get('CName'),
                        EName: data[i].get('EName'),
                        Type: data[i].get('Type'),
                        X: x,
                        Y: y,
                        Width: 170,//需要时可以是外部传入
                        LabelWidth: 60,//需要时可以是外部传入
                        Height:22,//需要时可以是外部传入
                        DataUrl:data[i].get('DataUrl')  
                    }, 'ZhiFang.model.FormItemParamsModel');
                   
                    store.add(rec);
                }
			}
		}
/*------------------------------------------------------------*/
		//JS生成GUID函数,类似.net中的NewID();
		function GetGUID(){   
		  return Ext.id(); 
		}
        
        //视图里的列表中的下拉列表控件的数据绑定
        function ComboBoxStore(combo,newValue){
            var combodd=combo;
        //var localData=[];
        Ext.Ajax.request({
            async:false,//非异步
            url:'../data/GetListDictionaryData.json',//url,
            method:'POST',
            timeout:5000,
            success:function(response,opts){
            var result = Ext.JSON.decode(response.responseText);
            if(result.success){
                var store = combo.store;
                store.removeAll();//删除原先的列
             var rec={};   
            Ext.each(result.list,function(model){
                    var rec = Ext.ModelMgr.create({
                        value: model.value,
                        text:model.text
                   }, 'ZhiFang.model.FormItemComboBoxModel');
                    store.add(rec);
                })
            }else{
                    Ext.Msg.alert('提示','获取信息失败！');
                }
            },
            failure : function(response,options){ 
                Ext.Msg.alert('提示','获取信息请求失败！');
            }
        });
        }
       
        
        //选择数据对象后,数据列表数据
        function CreateFormModelGridStore(combo,records){
          //var url = Ext.getCmp('FormParamsView_FormModel').getValue();
	        //数据对象:下拉列表的服务地址
	        var  myServerUrl=Ext.getCmp("FormParamsView_FormModel").getValue();
	        if(myServerUrl==""||myServerUrl==null){
	         Ext.Msg.alert("请填写配置好服务地址再操作!");
	         return;
	        }
                    
          var FormModelGrid = Ext.getCmp('FormParamsView_FormModelGrid');
          var Object = records[0].get('Object');
        //var localData=[];
        Ext.Ajax.request({
            async:false,//非异步
            url:myServerUrl,
            
            //部门参数
//            params: {
//                 newId:Ext.id()
//            },
            
            method:'POST',
            timeout:5000,
            success:function(response,opts){
            var result = Ext.JSON.decode(response.responseText);
           
            if(result.success){
                var store = FormModelGrid.store;
                store.removeAll();//删除原先的列
             var rec={};   
            Ext.each(result.list,function(model){
                    var rec = Ext.ModelMgr.create({
                        Id:GetGUID(),
                        CName: model.CName,
                        EName:model.EName,
                        Type:'TextField'
   
                   }, 'ZhiFang.model.FormItemParamsModel');
                    store.add(rec);
                })
            }else{
                    Ext.Msg.alert('提示','获取信息失败！');
                }
            },
            failure : function(response,options){ 
                Ext.Msg.alert('提示','获取信息请求失败！');
            }
        });
        
   }
  
	}
})