/**
 * BS血库系统配置
 * @author longfc
 * @version 2020-02-15
 */
var JcallShell = JcallShell || {};
JcallShell.System = JcallShell.System || {};
/**系统语言*/
JcallShell.System.Lang = 'CN';
/**系统信息*/
JcallShell.System.Name = '血库系统';
/**系统编号*/
JcallShell.System.CODE = 'BLTF';
JcallShell.BLTF = JcallShell.BLTF || {};

/***
 * 获取某一window的最顶级的父窗体对象
 * @param {Object} curWin:当前window对象
 */
if(JcallShell.System.getTop == undefined) {
	JcallShell.System.getTop = function(curWin) {
		var curWin = curWin || window;
		var win = curWin.top == curWin ? curWin : JcallShell.System.getTop(curWin.top);
		return win;
	};
}

if(!JcallShell.BLTF.ClassDict) {
	JcallShell.BLTF.ClassDict = {
		//命名空间域名
		_classNameSpace: 'ZhiFang.Entity.BloodTransfusion',
		/** @public
		 * 初始化字典信息，支持单个字典，也支持多个字典
		 * @param {Object} className 类名
		 * @param {Object} callback 回调函数
		 * @example
		 * 	JcallShell.System.ClassDict.init(
		 * 		'PContractStatus',
		 * 		function(){
		 * 			//回调函数处理
		 * 		}
		 * 	);
		 * 	JcallShell.System.ClassDict.init(
		 * 		['PContractStatus','PTaskStatus'],
		 * 		function(){
		 * 			//回调函数处理
		 * 		});
		 */
		init: function(className, callback) {
			var me = JcallShell.System.ClassDict;
			var type = Ext.typeOf(className);

			if(type == 'string') {
				className = [className];
			}

			var hasData = true;

			for(var i in className) {
				className[i] = {
					classnamespace: this._classNameSpace,
					classname: className[i]
				};
				if(!me[className[i].classname]) {
					hasData = false;
				}
			}

			if(hasData) {
				if(Ext.typeOf(callback) == 'function') {
					callback();
				}
			} else {
				JcallShell.System.ClassDict.loadClassInfoList(className, callback);
			}
		},
		/** @public
		 * 根据字典内容ID获取字典内容
		 * @param {Object} className
		 * @param {Object} id
		 */
		getClassInfoById: function(className, id) {
			return JcallShell.System.ClassDict.getClassInfoById(className, id);
		},
		/** @public
		 * 根据字典内容Name获取字典内容
		 * @param {Object} className
		 * @param {Object} name
		 */
		getClassInfoByName: function(className, name) {
			return JcallShell.System.ClassDict.init(className, name);
		}
	};
}

if(!JcallShell.BLTF.StatusList) {
	/**系统业务状态集合*/
	JcallShell.BLTF.StatusList = {
		getBaseInfo: function() {
			return {
				List: [], //["", '全部', 'font-weight:bold;color:black;text-align:center;']
				Enum: {}, //{Id:Name}
				BGColor: {}, //{Id:BGColor}
				FColor: {} //{Id:FontColor}
			};
		},
		/**业务状态集合*/
		Status: {
			/**身份类型*/
			BloodIdentityType: {
				List: [],
				Enum: {}, //{Id:Name}
				BGColor: {}, //{Id:BGColor}
				FColor: {} //{Id:FontColor}
			},
			/**修改记录操作类型*/
			UpdateOperationType: {
				List: [],
				Enum: {}, //{Id:Name}
				BGColor: {}, //{Id:BGColor}
				FColor: {} //{Id:FontColor}
			},
			/**血袋登记操作类型*/
			BloodBagOperationType: {
				List: [],
				Enum: {}, //{Id:Name}
				BGColor: {}, //{Id:BGColor}
				FColor: {} //{Id:FontColor}
			}
		},
		/**获取申请总单状态参数*/
		getParams: function(classname) {
			var me = this,
				params = {};
			params = {
				"jsonpara": [{
					"classname": classname,
					"classnamespace": "ZhiFang.Entity.BloodTransfusion"
				}]
			};
			return params;
		},
		/***
		 * 获取BLTF每年的业务状态信息
		 * @param {Object} classname 业务状态类名称
		 * @param {Object} isRefresh 是否重新获取
		 * @param {Object} hasAll 是否添加全部选择项
		 * @param {Object} callback
		 */
		getStatusList: function(classname, isRefresh, hasAll, callback) {
			//var me = this;
			//运行参数编码为空
			var result = {
				success: true,
				value: JcallShell.BLTF.StatusList.getBaseInfo(),
				msg: ""
			};
			if(!classname) {
				result.success = false;
				if(callback) callback(result);
				return;
			}
			//classname不存在Lists
			if(!JcallShell.BLTF.StatusList.Status[classname]) {
				result.success = false;
				if(callback) callback(result);
				return;
			}
			//运行参数值已存在,且不用重新调用服务获取,直接返回
			var tempStatus = JcallShell.BLTF.StatusList.Status[classname];
			if(!tempStatus || !tempStatus.List || tempStatus.List.length <= 0) isRefresh = true;
			if(tempStatus.List && isRefresh != true) {
				result.success = true;
				result.value = tempStatus;
				if(callback) callback(result);
				return;
			}

			var params = {},
				url = JcallShell.System.Path.getRootUrl(JcallShell.System.ClassDict._classDicListUrl);
			params = Ext.encode(JcallShell.BLTF.StatusList.getParams(classname));
			JcallShell.BLTF.StatusList.Status[classname].List = [];
			JcallShell.BLTF.StatusList.Status[classname].Enum = {};
			JcallShell.BLTF.StatusList.Status[classname].FColor = {};
			JcallShell.BLTF.StatusList.Status[classname].BGColor = {};
			var tempArr = [];
			JcallShell.Server.post(url, params, function(data) {
				if(data.success && data.value && data.value[0][classname].length > 0) {
					if(hasAll) {
						JcallShell.BLTF.StatusList.Status[classname].List.push(["", '全部',
							'font-weight:bold;color:black;text-align:center;'
						]);
					}
					Ext.Array.each(data.value[0][classname], function(obj, index) {
						var style = ['font-weight:bold;text-align:center;'];
						if(obj.FontColor) {
							JcallShell.BLTF.StatusList.Status[classname].FColor[obj.Id] = obj.FontColor;
						}
						if(obj.BGColor) {
							style.push('color:' + obj.BGColor); //background-
							JcallShell.BLTF.StatusList.Status[classname].BGColor[obj.Id] = obj.BGColor;
						}
						JcallShell.BLTF.StatusList.Status[classname].Enum[obj.Id] = obj.Name;
						tempArr = [obj.Id, obj.Name, style.join(';')];
						JcallShell.BLTF.StatusList.Status[classname].List.push(tempArr);
					});
					result.success = true;
					result.value = JcallShell.BLTF.StatusList.Status[classname];
					if(callback) callback(result);
				}
			}, false);
		}
	};
}

/**系统运行参数*/
if(!JcallShell.BLTF.RunParams) {
	JcallShell.BLTF.RunParams = {
		//系统运行参数集合信息
		JObjectList: null,
		//是否初始化运行参数
		ISInitRunParams: false,
		//初始化运行参数总数
		InitRunParamsCount: 4,
		/**初始化系统运行参数的某一个运行参数获取完成后处理*/
		getRunParamsAfter: function(callback, count) {
			if(count == JcallShell.BLTF.RunParams.InitRunParamsCount) {
				JcallShell.BLTF.RunParams.ISInitRunParams = true;
				JcallShell.BLTF.cachedata.setCache("ISInitRunParams", true);
				if(callback) callback();
			}
		},
		/**系统登录后初始化部分系统运行参数信息*/
		initRunParams: function(callback) {
			var isInitRunParams = JcallShell.BLTF.cachedata.getCache("ISInitRunParams");
			if(JcallShell.BLTF.RunParams.ISInitRunParams == true || isInitRunParams == true) {
				if(callback) {
					return callback();
				} else {
					return;
				}
			}

			var count = 0;
			//系统运行参数"启用用户UI配置"
			JcallShell.BLTF.RunParams.getRunParamsValue("EnableUserUIConfig", false, function(data) {
				if(data.value && data.value.ParaValue && data.value.ParaValue == 1) {
					JcallShell.BLTF.BUserUIConfig.getUIConfigByUSERID("", null);
				}
				count++;
				JcallShell.BLTF.RunParams.getRunParamsAfter(callback, count);
			});
			//系统运行参数"列表默认分页记录数"
			JcallShell.BLTF.RunParams.getRunParamsValue("BLTFUIDefaultPageSize", false, function(data) {
				count++;
				JcallShell.BLTF.RunParams.getRunParamsAfter(callback, count);
			});
			//系统运行参数"血袋扫码识别字段"
			JcallShell.BLTF.RunParams.getRunParamsValue("BloodBagScanCodeIDField", false, function(data) {
				count++;
				JcallShell.BLTF.RunParams.getRunParamsAfter(callback, count);
			});
			//系统运行参数"输血登记是否需要交接登记完成后"
			JcallShell.BLTF.RunParams.getRunParamsValue("BloodTransISNeedBloodBag", false, function(data) {
				count++;
				JcallShell.BLTF.RunParams.getRunParamsAfter(callback, count);
			});
		},
		getRunParamsList: function() {},
		/**旧的运行参数集合*/
		Lists: {
			/**@description 集成平台服务访问URL*/
			IPlatformServiceAccessURL: {
				Id: "BL-SYSE-LURL-0002",
				CName: "集成平台服务访问URL"
			},
			/**列表默认分页记录数*/
			BLTFUIDefaultPageSize: {
				Id: "BL-LRMP-UIPA-0007",
				CName: "列表默认分页记录数"
			},
			/**@description 启用用户UI配置*/
			EnableUserUIConfig: {
				Id: "BL-EUSE-UICF-0008",
				CName: "启用用户UI配置"
			},
			/**@description 是否允许手工选择患者ABO及患者Rh*/
			ISALLOWPATABOANDRHOPT: {
				Id: "BL-NULL-ISBH-0021",
				CName: "是否允许手工选择患者ABO及患者Rh"
			},
			/**@description 血袋扫码识别字段*/
			BloodBagScanCodeIDField: {
				Id: "BL-BBSC-IDFT-0022",
				CName: "血袋扫码识别字段"
			},
			/**@description 输血登记是否需要交接登记完成后*/
			BloodTransISNeedBloodBag: {
				Id: "BL-BLTF-ISNB-0023",
				CName: "输血登记是否需要交接登记完成后"
			}
		},
		/***
		 * 获取运行参数值
		 * @param {Object} paraKey 运行参数key
		 * @param {Object} isRefresh 是否重新获取运行参数值
		 * @param {Object} callback 回调
		 */
		getRunParamsValue: function(paraKey, isRefresh, callback) {
			var me = this;
			//运行参数编码为空
			var result = {
				success: true,
				value: {
					ParaValue: null
				},
				msg: ""
			};
			if(!paraKey) {
				result.success = false;
				if(callback) {
					return callback(result);
				} else {
					return;
				}
			}
			if(!isRefresh) {
				var paraValue1 = JcallShell.BLTF.cachedata.getCache(paraKey);
				if(!paraValue1) isRefresh = true;
				//运行参数值已存在,且不用重新调用服务获取,直接返回
				if(!isRefresh) {
					result.success = true;
					result.value.ParaValue = paraValue1;
					if(callback) {
						return callback(result);
					} else {
						return;
					}
				}
			}
			var url = JcallShell.System.Path.ROOT + "/SingleTableService.svc/ST_UDTO_SearchBParameterByByParaNo?paraNo=" +
				JcallShell.BLTF.RunParams.Lists[paraKey].Id + "&t=" + (new Date().getTime());
			JcallShell.Server.get(url, function(data) {
				var paraValue = "";
				if(data.success) {
					var obj = data.value;
					if(obj.ParaValue) paraValue = obj.ParaValue;
					if(!paraValue && paraValue != 0) paraValue = "";
					JcallShell.BLTF.cachedata.setCache(paraKey, paraValue);
				}
				JcallShell.BLTF.RunParams.Lists[paraKey].Value = paraValue;
				if(callback) {
					return callback(data);
				} else {
					return;
				}
			});
		}
	};
}

/**系统运行内存缓存信息*/
if(!JcallShell.BLTF.RunInfo) {
	JcallShell.BLTF.RunInfo = {
		//系统运行初始化
		initAll: function(callback) {
			//初始化字典信息
			this.initDictList();
			//初始化运行参数
			JcallShell.BLTF.RunParams.initRunParams(callback);
		},
		//初始化字典集合信息
		initDictList: function() {

		}
	};
}

/**当前登录用户的UI配置信息*/
if(!JcallShell.BLTF.BUserUIConfig) {
	JcallShell.BLTF.BUserUIConfig = {
		List: {},
		addListByKey: function(userUIKey, params) {
			if(!userUIKey || !params) return;
			
			var empID = JcallShell.System.Cookie.get(JcallShell.System.Cookie.map.USERID);
			var userUIKey1=empID+userUIKey;
			if(!JcallShell.BLTF.BUserUIConfig.List) JcallShell.BLTF.BUserUIConfig.List = {};
			JcallShell.BLTF.BUserUIConfig.List[userUIKey1] = params;
		},
		removeByKey: function(userUIKey) {
			if(!userUIKey) return;
			
			var empID = JcallShell.System.Cookie.get(JcallShell.System.Cookie.map.USERID);
			var userUIKey1=empID+userUIKey;
			if(!JcallShell.BLTF.BUserUIConfig.List) JcallShell.BLTF.BUserUIConfig.List = {};
			if(JcallShell.BLTF.BUserUIConfig.List[userUIKey1]) delete JcallShell.BLTF.BUserUIConfig.List[userUIKey1];
		},
		getUIConfigByKey: function(userUIKey, isRefresh, callback) {
			var empID = JcallShell.System.Cookie.get(JcallShell.System.Cookie.map.USERID);
			var userUIKey1=empID+userUIKey;
			var userUI = JcallShell.BLTF.BUserUIConfig.List[userUIKey1];
			if(!userUI) userUI = JcallShell.BLTF.cachedata.getCache(userUIKey1);

			if(!userUI) isRefresh = true;
			if(userUI && isRefresh != true) {
				if(callback) callback(userUI);
				return;
			}
			JcallShell.BLTF.BUserUIConfig.getUIConfigByUSERID(userUIKey, function(userUI2) {
				if(callback) callback(userUI2);
			});
		},
		getUIConfigByUSERID: function(userUIKey, callback) {
			var userUI = null;
			var arr = [];
			if(userUIKey && userUIKey != '') {
				arr.push("buseruiconfig.UserUIKey='" + userUIKey + "'");
			}
			var empID = JcallShell.System.Cookie.get(JcallShell.System.Cookie.map.USERID);
			if(empID) {
				arr.push("buseruiconfig.EmpID=" + empID);
			}
			var where = arr.join(") and (");
			if(where) where = "(" + where + ") and buseruiconfig.IsUse=1";
			var fields =
				"BUserUIConfig_Id,BUserUIConfig_UserUIKey,BUserUIConfig_UserUIName,BUserUIConfig_UITypeID,BUserUIConfig_EmpID,BUserUIConfig_Comment";
			var url = JcallShell.System.Path.getRootUrl(
				"/BloodTransfusionManageService.svc/BT_UDTO_SearchBUserUIConfigByHQL?isPlanish=true&fields=" + fields +
				"&where=" + where
			);
			JcallShell.Server.get(url, function(data) {
				var list = null;
				if(data.success && data.value && data.value.list)
					list = data.value.list;
				if(list && list.length > 0) {
					var userUIKey1=empID+userUIKey;
					JcallShell.BLTF.cachedata.setCache(userUIKey1, list[0]);
					JcallShell.BLTF.BUserUIConfig.List[userUIKey1] = list[0];
					if(userUIKey1) {
						userUI = JcallShell.BLTF.cachedata.getCache(userUIKey1);
						if(!userUI)userUI = JcallShell.BLTF.BUserUIConfig.List[userUIKey1];
					}
				}
				if(callback) callback(userUI);
			}, false);
		}
	};
}

if(!JcallShell.BLTF.cachedata) {
	JcallShell.BLTF.cachedata = {
		SYS_KEY: "BLTF_SYS",
		SYS_CACHE_KEY: "CacheData",
		commonDataKey: "CacheData",
		/***
		 * 获取某一window的最顶级的父窗体对象
		 * @param {Object} curWin:当前window对象
		 */
		getTop: function(curWin) {
			var me = this;
			curWin = curWin || window;
			var win = curWin.top == curWin ? curWin : me.getTop(curWin.top);
			return win;
		},
		/***
		 * 设置父窗体对象(window)的缓存数据(CacheData)
		 * @param {Object} dictKey:CacheData的key
		 * @param {Object} data:需要缓存的数据信息
		 */
		setCache: function(dictKey, data, win) {
			var me = this;
			if(!win) win = window;
			win = me.getTop(win);
			if(!dictKey) dictKey = me.commonDataKey;
			if(!win[me.SYS_KEY]) win[me.SYS_KEY] = {};
			if(!win[me.SYS_KEY][me.SYS_CACHE_KEY]) win[me.SYS_KEY][me.SYS_CACHE_KEY] = {};
			win[me.SYS_KEY][me.SYS_CACHE_KEY][dictKey] = data;
		},
		/***
		 * 获取父窗体对象(window)的缓存数据(CacheData)
		 * @param {Object} dictKey:CacheData的key
		 */
		getCache: function(dictKey, win) {
			var me = this;
			if(!win) win = window;
			win = me.getTop(win);
			var data = "";
			if(!dictKey) dictKey = me.commonDataKey;
			if(!win) return data;
			if(!win[me.SYS_KEY]) win[me.SYS_KEY] = {};

			if(win[me.SYS_KEY][me.SYS_CACHE_KEY]) {
				data = win[me.SYS_KEY][me.SYS_CACHE_KEY][dictKey];
			}
			return data;
		},
		/***
		 * 删除父窗体对象(window)的缓存数据(CacheData)
		 * @param {Object} dictKey
		 */
		delete: function(dictKey, win) {
			var me = this;
			if(!win) win = window;
			win = me.getTop(win);
			if(!win) return;
			if(!win[me.SYS_KEY]) win[me.SYS_KEY] = {};
			if(win[me.SYS_KEY][me.SYS_CACHE_KEY]) {
				if(dictKey) {
					delete win[me.SYS_KEY][me.SYS_CACHE_KEY][dictKey];
				} else {
					win[me.SYS_KEY][me.SYS_CACHE_KEY] = {};;
				}
			}
		}
	};
}

;
(function(win) {
	win.JcallShell = win.JShell = JcallShell;
	//语言包处理，默认加载中文语言包
	var params = JShell.Page.getParams(true);
	if(params.LANG) {
		JcallShell.System.Lang = params.LANG;
	}
	//加载语言
	JcallShell.Page.changeLangage(JcallShell.System.Lang);
})(window);