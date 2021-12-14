/**
 * 医院表单
 * @author Jcall
 * @version 2016-12-27
 */
Ext.define('Shell.class.weixin.hospital.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],

	title: '医院信息',
	width: 430,
	height: 350,
//	bodyPadding: 10,
    bodyPadding:'10px 10px 10px 5px',
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBHospitalById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ServerWCF/DictionaryService.svc/ST_UDTO_AddBHospital',
	/**修改服务地址*/
	editUrl: '/ServerWCF/DictionaryService.svc/ST_UDTO_UpdateBHospitalByField',

	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
    AreaID:null,
	/**布局方式*/
//	layout: 'anchor',
    layout:{
        type:'table',
        columns:2//每行有几列
    },
	/**每个组件的默认属性*/
	defaults: {
//		anchor: '100%',
        width:200,
		labelWidth: 65,
		labelAlign: 'right'
	},

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//表单监听
		me.initFromListeners();
	},

	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this;

		//医院图像
		var items = [{
			fieldLabel: '医院名称',
			name: 'BHospital_Name',
			itemId: 'BHospital_Name',
			colspan:2,
			width: me.defaults.width * 2,
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '英文名称',
			colspan:1,
			name: 'BHospital_EName'
		}, {
			fieldLabel: '医院简称',
			colspan:1,
			name: 'BHospital_SName'
		}, {
			fieldLabel: '拼音字头',
			colspan:1,
			name: 'BHospital_PinYinZiTou'
		}, {
			fieldLabel: '快捷码',
			colspan:1,
			name: 'BHospital_Shortcode'
		}, {
			fieldLabel: '医院位置',
			name: 'BHospital_PostionID',
			itemId: 'BHospital_PostionID',
			colspan:1,
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.weixin.hospital.Map'
		}, {
			fieldLabel: '医院地址',
			hidden: true,
			name: 'BHospital_Postion',
			itemId: 'BHospital_Postion'
		}, {
			fieldLabel: '医院编码',
			name: 'BHospital_HospitalCode',
			colspan:1,
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '所属城市',
			xtype: 'uxCheckTrigger',
			emptyText:'必填项',allowBlank:false,
			colspan:1,
			name: 'BHospital_CityName',
			itemId: 'BHospital_CityName',
			className: 'Shell.class.sysbase.country.province.city.CheckApp'
		}, {
			fieldLabel: '所属城市主键ID',
			hidden: true,
			name: 'BHospital_CityID',
			itemId: 'BHospital_CityID'
		}, {
			fieldLabel: '医院分类',
			xtype: 'uxCheckTrigger',
			//emptyText:'必填项',allowBlank:false,
			name: 'BHospital_HTypeName',
			itemId: 'BHospital_HTypeName',
			colspan:1,
			className: 'Shell.class.weixin.hospital.type.CheckGrid'
		}, {
			fieldLabel: '医院分类主键ID',
			hidden: true,
			name: 'BHospital_HTypeID',
			itemId: 'BHospital_HTypeID'
		}, {
			fieldLabel: '医院级别',
			xtype: 'uxCheckTrigger',
			//emptyText:'必填项',allowBlank:false,
			name: 'BHospital_LevelName',
			itemId: 'BHospital_LevelName',
			colspan:1,
			className: 'Shell.class.weixin.hospital.level.CheckGrid'
		}, {
			fieldLabel: '医院级别主键ID',
			hidden: true,
			name: 'BHospital_LevelID',
			itemId: 'BHospital_LevelID'
		},{
			boxLabel: '是否使用',
			name: 'BHospital_IsUse',
			xtype: 'checkbox',
			colspan:1,
			width: me.defaults.width * 1,
			checked: true,
			fieldLabel: ' ',
			labelSeparator:''
		},{
			fieldLabel: '区域',
			xtype: 'uxCheckTrigger',
			//emptyText:'必填项',allowBlank:false,
			name: 'BHospital_Area',
			itemId: 'BHospital_Area',
			hidden:true,
			colspan:1,
			className: 'Shell.class.weixin.hospital.area.CheckGrid'
		}, {
			fieldLabel: '区域主键ID',
			hidden: true,
			name: 'BHospital_AreaID',
			itemId: 'BHospital_AreaID'
		},  {
			fieldLabel: '医院描述',
			height: 85,
			colspan:2,
			width: me.defaults.width * 2,
//			labelAlign: 'top',
			name: 'BHospital_Comment',
			xtype: 'textarea'
		},{
			fieldLabel: '主键ID',
			name: 'BHospital_Id',
			hidden: true
		}];

		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();

		var entity = {
			Name: values.BHospital_Name,
			EName: values.BHospital_EName,
			SName: values.BHospital_SName,
			PinYinZiTou: values.BHospital_PinYinZiTou,
			Shortcode: values.BHospital_Shortcode,
			HospitalCode: values.BHospital_HospitalCode,
			IsUse: values.BHospital_IsUse ? true : false,
			Comment: values.BHospital_Comment,
			CityName:values.BHospital_CityName,
			CityID:values.BHospital_CityID
		};

        
		if(values.BHospital_HTypeName) {
			entity.HTypeName = values.BHospital_HTypeName;
			entity.HTypeID = values.BHospital_HTypeID;
		}
		if(values.BHospital_LevelName) {
			entity.LevelName = values.BHospital_LevelName;
			entity.LevelID = values.BHospital_LevelID;
		}
		if(values.BHospital_Postion) {
			entity.Postion = values.BHospital_Postion;
		}
		//区域
		if(me.AreaID) {
			entity.AreaID = me.AreaID;
		}
	
		return {
			entity: entity
		};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams(),
			fieldsArr = [];

        var  fields=['Name','EName','SName','PinYinZiTou','Shortcode','Postion','HospitalCode',
        'CityName','CityID','HTypeName','HTypeID','LevelName','LevelID','AreaID',
        'Comment','IsUse','Id'];
		entity.fields = fields.join(',');
       
		entity.entity.Id = values.BHospital_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
       //坐标设置
		var Postion = me.getComponent('BHospital_Postion'),
			PostionID = me.getComponent('BHospital_PostionID');
		if(data.BHospital_Postion) {
			var geocoder = new qq.maps.Geocoder({
				complete: function(result) {
					PostionID.setValue(result.detail.address);
				}
			});
			var strPointArray = data.BHospital_Postion.split(',');
			var point1 = Number(strPointArray[0]);
			var point2 = Number(strPointArray[1]);
			var coord = new qq.maps.LatLng(point1, point2);
			geocoder.getAddress(coord);
		} else {
			data.BHospital_PostionID = '';
		}
	    var AreaID = me.getComponent('BHospital_AreaID'),
	    Area = me.getComponent('BHospital_Area');
	    if(data.BHospital_AreaID){
	    	me.getClientEleArea(data.BHospital_AreaID,function(data){
	    		if(data.value){
	    			Area.setValue(data.value.list[0].ClientEleArea_AreaCName);
	    		}
	    	});
	    } 
		return data;
	},
	/**初始化表单监听*/
	initFromListeners: function() {
		var me = this;

		//拼音字头
		var BHospital_Name = me.getComponent('BHospital_Name');
		BHospital_Name.on({
			change: function(field, newValue, oldValue, eOpts) {
				if(newValue != "") {
					JShell.Action.delay(function() {
						JShell.System.getPinYinZiTou(newValue, function(value) {
							me.getForm().setValues({
								BHospital_PinYinZiTou: value,
								BHospital_Shortcode: value
							});
						});
					}, null, 200);
				} else {
					me.getForm().setValues({
						BHospital_PinYinZiTou: "",
						BHospital_Shortcode: ""
					});
				}
			}
		});
		
		//所属城市
		var BHospital_CityName = me.getComponent('BHospital_CityName'),
			BHospital_CityID = me.getComponent('BHospital_CityID');

		if(BHospital_CityName) {
			BHospital_CityName.on({
				check: function(p, record) {
					BHospital_CityName.setValue(record ? record.get('BCity_Name') : '');
					BHospital_CityID.setValue(record ? record.get('BCity_Id') : '');
					p.close();
				}
			});
		}
		
		//医院分类
		var BHospital_HTypeName = me.getComponent('BHospital_HTypeName'),
			BHospital_HTypeID = me.getComponent('BHospital_HTypeID');

		if(BHospital_HTypeName) {
			BHospital_HTypeName.on({
				check: function(p, record) {
					BHospital_HTypeName.setValue(record ? record.get('BHospitalType_Name') : '');
					BHospital_HTypeID.setValue(record ? record.get('BHospitalType_Id') : '');
					p.close();
				}
			});
		}

		//医院等级
		var BHospital_LevelName = me.getComponent('BHospital_LevelName'),
			BHospital_LevelID = me.getComponent('BHospital_LevelID');

		if(BHospital_LevelName) {
			BHospital_LevelName.on({
				check: function(p, record) {
					BHospital_LevelName.setValue(record ? record.get('BHospitalLevel_Name') : '');
					BHospital_LevelID.setValue(record ? record.get('BHospitalLevel_Id') : '');
					p.close();
				}
			});
		}
		
		//区域
		var ClientEleArea = me.getComponent('BHospital_Area'),
		    ClientEleAreaID = me.getComponent('BHospital_AreaID');
		    
		if(ClientEleArea) {
			ClientEleArea.on({
				check: function(p, record) {
					ClientEleArea.setValue(record ? record.get('ClientEleArea_AreaCName') : '');
					ClientEleAreaID.setValue(record ? record.get('ClientEleArea_Id') : '');
					p.close();
				}
			});
		}   
		    
		//坐标设置
		var Postion = me.getComponent('BHospital_Postion'),
			PostionID = me.getComponent('BHospital_PostionID');
		if(PostionID) {
			PostionID.on({
				check: function(win, record) {
					Postion.setValue(win.MapLatlng);
					PostionID.setValue(win.MapAddress);
					win.close();
				},
				beforetriggerclick:function(win){
					if(PostionID.getValue()){
						win.classConfig={MapAddress:PostionID.getValue()};
					}else{
						win.classConfig={MapAddress:JcallShell.System.BHOSPITALPOSTION};
					}
				}
			});
		}
	},
	/**获取区域信息*/
	getClientEleArea: function(Id,callback) {
		var me = this,
			url = JShell.System.Path.getRootUrl('/ServerWCF/DictionaryService.svc/ST_UDTO_SearchClientEleAreaByHQL?isPlanish=true');
			
		url += '&fields=ClientEleArea_AreaCName,ClientEleArea_Id';
		url += '&where=clientelearea.Id='+Id;

		JcallShell.Server.get(url, function(data) {
			if(data.success) {
				callback(data);
			} 
		});
	}
});