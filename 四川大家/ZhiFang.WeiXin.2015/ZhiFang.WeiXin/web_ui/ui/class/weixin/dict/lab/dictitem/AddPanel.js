/**
 * 项目表单和组套项目
 * @author liangyl
 * @version 2018-02-01
 */
Ext.define('Shell.class.weixin.dict.lab.dictitem.AddPanel',{
    extend:'Shell.ux.panel.AppPanel',
    title:'项目表单和组套项目',
    border:false,
     /**新增服务地址*/
    addUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_AddBLabTestItemVO',
    /**修改服务地址*/
    editUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_UpdateBLabTestItemByFieldVO', 
    /**是否判断是否执行价格列修改，*/
    IsPrice:true,
      /**是否是内部价格列修改*/
    IsGreatMasterPrice:true,
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.showMask();
		me.Form.on({
			onSaveClick: function(){
		    	me.onSave();
		    },
		    beforeMarketPriceChange:function(com){
		    	//三甲价格如果组套细项有值，表单三甲价格不能修改，表单项目价格设置为只读
		    	if(me.Grid.getStore().getCount()>0){
		    		me.Form.IsMarketPriceReadOnly='1';
		    		com.setReadOnly(true);
		    	}
		    },
		    changePriceClick: function(){
		    	JShell.Action.delay(function(){
		            me.formChangePrice();
		        },null,100);
		    },
		    changeDiscountPriceClick: function(){
		    	JShell.Action.delay(function(){
		    	    me.formChangeDiscountPrice();
		    	},null,100);
		    },
		    changeGreatMasterPriceClick: function(){
		    	JShell.Action.delay(function(){		    		
		    	    me.formChangeMasterPrice();
		    	},null,100);
		    },
		    changeDiscountGreatMasterPriceClick: function(){
		    	JShell.Action.delay(function(){
                    me.formDiscountGreatMasterPrice();
                },null,100);
		    }
		});
        me.Grid.on({
        	load:function(grid){
        	    JShell.Action.delay(function(){
                    me.CountGridGreatMasterPrice();
                },null,100);
        	},
        	editClick:function(grid){
//      		me.clearPriceData();
        		grid.onEditClick();
        	},
			update:function(grid){
				me.changeFormPrice2(grid);
				var bo =false ;
				var count =grid.getStore().getCount();
				if(count>0)bo=true;
				me.setFormValue(bo);
			},
			changePrice:function(){
				JShell.Action.delay(function(){
					me.changeGridPrice(me.Grid);
		        },null,100);
			},
			changeGreatMasterPrice:function(){
				JShell.Action.delay(function(){
//				    me.changeGridGreatMasterPrice(me.Grid);
		        },null,100);
			},
			onDelClick:function(grid){
				JShell.Action.delay(function(){
                    me.changeFormPrice2(me.Grid);
				    var bo =false ;
					var count =me.Grid.getStore().getCount();
					if(count>0)bo=true;
					me.setFormValue(bo);
		        },null,200);
			},
			changeResult:function(grid,data){
				var buttonsToolbar = me.Grid.getComponent('buttonsToolbar');
				var labMarketPrice = buttonsToolbar.getComponent('labMarketPrice');
				var labGreatMasterPrice = buttonsToolbar.getComponent('labGreatMasterPrice');
				var labPrice = buttonsToolbar.getComponent('labPrice');
				if(data && data.list){
					me.Grid.getPriceData(data);
				}else{
					labMarketPrice.setValue('0');	
					labGreatMasterPrice.setValue('0');
					labPrice.setValue('0');
				}
			}
		});
	},
	
	initComponent:function(){
		var me = this;
	    me.addEvents('save');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		me.Form = Ext.create('Shell.class.weixin.dict.lab.dictitem.Form', {
			region: 'north',
			header: false,height:405,
			itemId: 'Form',split: true,
			collapsible: true
		});
		me.Grid = Ext.create('Shell.class.weixin.dict.lab.dictitem.LabGroupItemGrid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
			
		});
		return [me.Form,me.Grid];
	},
	/**
	 * 右边面板数据加载
	 * id 左边面板的选择行id
	 * ClienteleID 实验室
	 * ItemNo左边面板的项目编号
	 * */
	loadData:function(id,ItemNo,ClienteleID){
		var me=this;
		me.clearData();
		me.Grid.canEdit=false;
		if(id)me.Form.isShow(id);
	    if(ItemNo && ClienteleID){
	    	me.Grid.ItemNo=ItemNo;
	        me.Grid.ClienteleID=ClienteleID;
	        me.Grid.onSearch();
	    }
	    var buttonsToolbar = me.Grid.getComponent('buttonsToolbar');
		var Edit=buttonsToolbar.getComponent('Edit');
		Edit.setDisabled(true);
		var btnDel=buttonsToolbar.getComponent('btnDel');
		btnDel.setDisabled(true);
	    me.hideMask();
	},
	clearData:function(){
		var me =this;
		me.showMask();
		me.Form.clearData();
        me.Grid.clearData();
        me.Grid.changePrice();
	},

	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		me.body.mask(text); //显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if(me.body) {
			me.body.unmask();
		} //隐藏遮罩层
	},
	onSave:function(){
		var me = this;
		if(!me.Form.getForm().isValid()) return;
		var isExec=true;
        //校验项目编号
         me.Form.getisValidItemNo(function(data){
        	if(data && data.value){
        		var len = data.value.list.length;
        		if(len>0){
        			var ItemNo=data.value.list[0].BLabTestItem_ItemNo;
        			isExec=false;
        			JShell.Msg.error('项目编号:【'+ItemNo+'】已存在！');
        		}
        	}
        });
        if(!isExec) return;
		me.showMask();
		//套餐项目信息
		var entity = me.Form.getAddParams();
	
		var	values = me.Form.getForm().getValues();
			//获取明细信息
		var entityInfo = me.Grid.getEntity(values.BLabTestItem_ItemNo);
        var fields = ['ItemNo', 'IsDoctorItem','CName',
			'StandardCode', 'IschargeItem', 'EName', 'ShortCode',
			'IsCombiItem', 'ShortName', 'IsProfile','Price','Unit',
			'IsCalc','MarketPrice','Prec','Cuegrade',
			'GreatMasterPrice','DiagMethod','Secretgrade',
			'BonusPercent','Color','Visible',
			'LabSuperGroupNo','DispOrder','UseFlag','OrderNo',
			'ItemDesc','Id','FWorkLoad','CostPrice'
		];
	    fields = fields.join(',');
		var obj={
            Unit:entity.Unit,
            CName:entity.CName,
            Color:entity.Color,
            Cuegrade:entity.Cuegrade,
            DiagMethod:entity.DiagMethod,
            EName:entity.EName,
            IsCalc:entity.IsCalc,
//          IsCombiItem:entity.IsCombiItem,
//          IsDoctorItem:entity.IsDoctorItem,
//          IsProfile:entity.IsProfile,
            IschargeItem:entity.IschargeItem,
            ItemDesc:entity.ItemDesc,
            ItemNo:entity.ItemNo,
			LabSubTestItem:entityInfo,
			ShortCode:entity.ShortCode,
			StandardCode:entity.StandardCode,
			ShortName:entity.ShortName,
			OrderNo:entity.OrderNo,
			UseFlag:entity.UseFlag,
			Visible:entity.Visible
		};
		obj.IsCombiItem=entity.IsCombiItem;
        obj.IsDoctorItem=entity.IsDoctorItem;
        obj.IsProfile=entity.IsProfile;
//		if(me.Grid.getStore().getCount()>0){
//			obj.IsCombiItem=1;
//			obj.IsProfile=1;
//  	    obj.IsDoctorItem=1;
//		}else{
//			obj.IsCombiItem=entity.IsCombiItem;
//          obj.IsDoctorItem=entity.IsDoctorItem;
//          obj.IsProfile=entity.IsProfile;
//		}
        if(entity.CostPrice){
			obj.CostPrice=entity.CostPrice;
		}
		if(entity.LabSuperGroupNo){
			obj.LabSuperGroupNo=entity.LabSuperGroupNo;
		}
		if(entity.DispOrder){
			obj.DispOrder=entity.DispOrder;
		}
		if(entity.BonusPercent){
			obj.BonusPercent=entity.BonusPercent;
		}
		if(entity.FWorkLoad){
			obj.FWorkLoad=entity.FWorkLoad;
		}
		if(entity.GreatMasterPrice){
			obj.GreatMasterPrice=entity.GreatMasterPrice;
		}
		if(entity.Price){
			obj.Price=entity.Price;
		}
		if(entity.MarketPrice){
			obj.MarketPrice=entity.MarketPrice;
		}
		if(values.BLabTestItem_Id){
			obj.Id=values.BLabTestItem_Id;
		}
		if(entity.LabCode){
			obj.LabCode= Number(entity.LabCode)
		}
		if(entity.Prec){
			obj.Prec= entity.Prec
		}
		if(entity.Secretgrade){
			obj.Secretgrade= entity.Secretgrade
		}
		if(entity.FWorkLoad){
			obj.FWorkLoad= entity.FWorkLoad
		}
		if(me.Form.formtype=='edit'){
			var url = JShell.System.Path.getUrl(me.editUrl);
			var params = Ext.JSON.encode({entity:obj,fields:fields});		
		}else{
			var url = JShell.System.Path.getUrl(me.addUrl);
			var params = Ext.JSON.encode({entity:obj});		
		}	
		JShell.Server.post(url,params,function(data){
			me.hideMask();//隐藏遮罩层
			if(data.success){
				me.clearData();
				me.fireEvent('save',me);
			    JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,1000);
			}else{
				me.fireEvent('saveerror',me);
				JShell.Msg.error(data.msg);
			}
		});
	},
	onAddClick:function(id){
		var me =this;
		me.clearData();
		me.hideMask();
		me.Form.formtype='add';
		me.Form.isAdd(id);
		me.Grid.canEdit=true;
		me.Grid.formtype='add';
		me.Form.IsMarketPriceReadOnly='0';
		me.Grid.clearData();
		var buttonsToolbar = me.Grid.getComponent('buttonsToolbar');
		var labMarketPrice = buttonsToolbar.getComponent('labMarketPrice');
		var labGreatMasterPrice = buttonsToolbar.getComponent('labGreatMasterPrice');
		var labPrice = buttonsToolbar.getComponent('labPrice');
	    labMarketPrice.setValue(0);	
		labGreatMasterPrice.setValue(0);
		labPrice.setValue(0);
		
		var Edit=buttonsToolbar.getComponent('Edit');
		Edit.enable();
		var btnDel=buttonsToolbar.getComponent('btnDel');
		btnDel.enable();
	},
	onEditClick:function(id,UseFlag,ItemNo,ClienteleID){
		var me=this;
		me.Grid.store.removeAll(); //清空数据
		me.Form.formtype='edit';
		me.Form.isEdit(id);
		me.Grid.formtype='edit';
		me.Grid.canEdit=true;
		var buttonsToolbar = me.Grid.getComponent('buttonsToolbar');
		var Edit=buttonsToolbar.getComponent('Edit');
		var btnDel=buttonsToolbar.getComponent('btnDel');
	    if(UseFlag=='1' || UseFlag=='true') {
	    	Edit.enable();
			btnDel.enable();
	    }
	    if(ItemNo && ClienteleID){
	    	me.Grid.ItemNo=ItemNo;
	        me.Grid.ClienteleID=ClienteleID;
	        me.Grid.onSearch();
	    }else{
	    	me.Grid.clearData();
	    }
	    var MarketPrice=me.Form.getComponent('BLabTestItem_MarketPrice');   	
    	//三甲价格如果组套细项有值，表单三甲价格不能修改，表单项目价格设置为只读
    	if(me.Grid.getStore().getCount()==0){
    		me.Form.IsMarketPriceReadOnly='0';
    		MarketPrice.setReadOnly(false);
    	}else{
    		me.Form.IsMarketPriceReadOnly='1';
    		MarketPrice.setReadOnly(true);
    	}
    	
	},
	//细项为空,清掉内部价格/折扣率，项目价格/折扣率
	clearPriceData:function(){
		var me =this;
		if(me.Grid.getStore().getCount()==0){
			me.Form.noLinkPrice('0');
	        me.Form.noLinkDiscountPrice('0');
	        me.Form.noLinkGreatMasterPrice('0');
	        me.Form.noLinkDiscountGreatMasterPrice('0');
		}
	},
	/**内部价格改变，需联动工具栏和表单内部价格*/
	changeGridGreatMasterPrice:function(grid){
		var me =this;
		me.IsGreatMasterPrice=false;
		me.Form.IsMarketPriceReadOnly='1';
	    me.changeGridText();
		var buttonsToolbar = me.Grid.getComponent('buttonsToolbar');
        var labPrice = buttonsToolbar.getComponent('labGreatMasterPrice');
        var v =labPrice.getValue();
        if(!v)  v=0	;
        var GreatMasterPrice = me.Form.getComponent('BLabTestItem_GreatMasterPrice');
        GreatMasterPrice.setValue(v);
        var MarketPrice = me.Form.getComponent('BLabTestItem_MarketPrice');

        //如果数据已全部清空，项目价格和内部价格可编辑
        if(grid.getStore().getCount()==0){
		    me.Form.IsMarketPriceReadOnly='0';
		    MarketPrice.setReadOnly(false);
        }
	},
	
	/**执行价格改变
	 * 从明细的执行价格开始--项目价格改变--项目价格折扣率改变
	 * 项目价格折扣率改变结束
	 * */
	changeGridPrice:function(grid){
		var me =this;
		me.Form.IsMarketPriceReadOnly='1';
	    me.changeGridText();
		var buttonsToolbar = me.Grid.getComponent('buttonsToolbar');
        var labPrice = buttonsToolbar.getComponent('labPrice');
        var v =labPrice.getValue();
        if(!v)  v=0	;
		var PriceCom = me.Form.getComponent('BLabTestItem_Price');
        me.IsPrice=false;	
        PriceCom.setValue(v);
        var MarketPrice = me.Form.getComponent('BLabTestItem_MarketPrice');
        var labGreatMasterPrice = buttonsToolbar.getComponent('labGreatMasterPrice');
        //如果数据已全部清空，三甲价格可编辑
        if(grid.getStore().getCount()==0){
		    me.Form.IsMarketPriceReadOnly='0';
		    MarketPrice.setReadOnly(false);
		    labGreatMasterPrice.setValue('0');
        }
	},
	changeGridText:function(){
    	var me= this;
    	var buttonsToolbar = me.Grid.getComponent('buttonsToolbar');
        var labPrice = buttonsToolbar.getComponent('labPrice');
        var labGreatMasterPrice = buttonsToolbar.getComponent('labGreatMasterPrice');
		var	records = me.Grid.store.data.items;
		var MarketPrice=0;GreatMasterPrice=0,Price=0;
		var count=0,len = records.length;
		for(var i=0;i<len;i++){
			Market=records[i].get('BLabGroupItemVO_BLabTestItemVO_MarketPrice');
			GreatMaster=records[i].get('BLabGroupItemVO_BLabTestItemVO_GreatMasterPrice');
			Price2=records[i].get('BLabGroupItemVO_Price');
			if(!Market) Market=0;
			if(!GreatMaster) GreatMaster=0;
			if(!Price2) Price2=0;
            MarketPrice+=Number(Market);
            GreatMasterPrice+=Number(GreatMaster);
            Price+=Number(Price2);
		}	
	    if(GreatMasterPrice>0)GreatMasterPrice=GreatMasterPrice.toFixed(4);
		if(Price>0)Price=Price.toFixed(4);
		Price = Ext.isNumber(Price) ? Price : parseFloat(String(Price).replace('.', '.'));
        Price = isNaN(Price) ? '' : String(Price).replace('.', '.');
        GreatMasterPrice = Ext.isNumber(GreatMasterPrice) ? Price : parseFloat(String(GreatMasterPrice).replace('.', '.'));
        GreatMasterPrice = isNaN(GreatMasterPrice) ? '' : String(GreatMasterPrice).replace('.', '.');
		labPrice.setValue(Price);
		labGreatMasterPrice.setValue(GreatMasterPrice);
    },
    changeFormText:function(){
    	var me= this;
    	//更新表单 大家价格，市场价格，折扣价格和组套项目 组合项目 医嘱项目
		var Obj= me.Grid.sumPrice();
		var GreatMasterPrice = me.Form.getComponent('BLabTestItem_GreatMasterPrice');
		var MarketPrice = me.Form.getComponent('BLabTestItem_MarketPrice');
		var Price = me.Form.getComponent('BLabTestItem_Price');
		var DiscountPrice = me.Form.getComponent('BLabTestItem_DiscountPrice');

		MarketPrice.setValue(Obj.MarketPrice);
		GreatMasterPrice.setValue(Obj.GreatMasterPrice);
		var DiscountPriceVal=DiscountPrice.getValue();
		if(DiscountPriceVal && DiscountPriceVal>0){
			me.Grid.store.each(function(record) {
			   me.Grid.changeDiscountPrice(DiscountPrice.getValue());
               me.Grid.changePrice();
			});
		}
    },
    //修改表单价格
    changeFormPrice2:function(grid){
    	var me= this;
    	//更新表单 大家价格，市场价格，折扣价格和组套项目 组合项目 医嘱项目
		var Obj= me.Grid.sumPrice();
		var MarketPrice = me.Form.getComponent('BLabTestItem_MarketPrice');

       //有细项重新计算内部价格和折扣价格
        if(grid.getStore().getCount()>0){
        	me.Form.IsMarketPriceReadOnly='1';
		    MarketPrice.setReadOnly(true);
			//改变内部价格,不联动内部价格折扣率，但不触发Focus事件
			me.Form.IsFocusGreatMasterPriceLoad=false;
			me.Form.noLinkGreatMasterPrice(Obj.GreatMasterPrice);
			me.Form.IsFocusGreatMasterPriceLoad=true;
		    //改变项目价格,不联动项目价格折扣率，但不触发Focus事件
			me.Form.IsbeforePriceChangeLoad=false;
			me.Form.noLinkPrice(Obj.Price);
			me.Form.IsbeforePriceChangeLoad=true;
//			
			me.Form.noLinkMasterPrice(Obj.MarketPrice);
			if(Obj.MarketPrice!=0){
				me.IsPrice=false;
		        me.formChangePrice();
		        me.IsGreatMasterPrice=false;
		        me.formChangeMasterPrice();
			}			
        }else{
        	//没有细项 删除项目价格，折扣率,内部价格，折扣率
        	me.Form.noLinkPrice(0);
			me.Form.noLinkDiscountPrice(0);
			me.Form.IsMarketPriceReadOnly='0';
		    MarketPrice.setReadOnly(false);
        }
        me.Grid.changePrice();
    },
    //修改表单价格
    changeFormPrice:function(){
    	var me= this;
    	//更新表单 大家价格，市场价格，折扣价格和组套项目 组合项目 医嘱项目
		var Obj= me.Grid.sumPrice();
		var GreatMasterPrice = me.Form.getComponent('BLabTestItem_GreatMasterPrice');
		var MarketPrice = me.Form.getComponent('BLabTestItem_MarketPrice');
		var Price = me.Form.getComponent('BLabTestItem_Price');
		var DiscountPrice = me.Form.getComponent('BLabTestItem_DiscountPrice');
        var DiscountGreatMasterPrice = me.Form.getComponent('BLabTestItem_DiscountGreatMasterPrice');
    	var buttonsToolbar = me.Grid.getComponent('buttonsToolbar');
	    var labMarketPrice =buttonsToolbar.getComponent('labMarketPrice');
		var labGreatMasterPrice = buttonsToolbar.getComponent('labGreatMasterPrice');
		var labPrice = buttonsToolbar.getComponent('labPrice');
        //有细项重新计算内部价格和折扣价格
        if(me.Grid.getStore().getCount()>0){
        	MarketPrice.IsMarketPrice=false;
			MarketPrice.setValue(Obj.MarketPrice);
			MarketPrice.IsMarketPrice=true;
			GreatMasterPrice.IsGreatMasterPriceLoad=false;
			GreatMasterPrice.setValue(Obj.GreatMasterPrice);
			GreatMasterPrice.IsGreatMasterPriceLoad=true;
        }else{
        	//没有细项 删除项目价格，折扣率,内部价格，折扣率
        	Price.IsPriceLoad=false;
			Price.setValue(Obj.Price);
			Price.IsPriceLoad=true;
			DiscountPrice.IsDiscountPrice=false;
			DiscountPrice.setValue('0');
			DiscountPrice.IsPriceLoad=true;
        }
    },
     /**是否是组套项目,是否是组合项目是否医嘱项目赋值
     * 有子项赋值为1，没有子项 赋值为0
     * */
    setFormValue:function(bo){
    	var me =this;
    	var buttonsToolbar = me.Grid.getComponent('buttonsToolbar');
		var IsCombiItem=me.Form.getComponent('BLabTestItem_IsCombiItem');
    	var IsProfile=me.Form.getComponent('BLabTestItem_IsProfile');
    	var IsDoctorItem=me.Form.getComponent('BLabTestItem_IsDoctorItem');
	    var MarketPrice = me.Form.getComponent('BLabTestItem_MarketPrice');
	    var labMarketPrice =buttonsToolbar.getComponent('labMarketPrice');
	    var GreatMasterPrice = me.Form.getComponent('BLabTestItem_GreatMasterPrice');

	    if(!bo){
		    MarketPrice.setValue('0');
		    GreatMasterPrice.setValue('0');
		    labMarketPrice.setValue('0');
	    }
//	    me.Form.setCombo(bo);
    },
   
    /**列表加载后处理
     * 给表单三甲价格赋值
     * 给内部价格赋值
     * */
    gridAfterLoad:function(){
    	var me =this;
		JShell.Action.delay(function(){
		  
    	   var Price = me.Form.getComponent('BLabTestItem_GreatMasterPrice');
    	   var DiscountGreatMasterPrice = me.Form.getComponent('BLabTestItem_DiscountGreatMasterPrice');
           me.Form.IsDiscountGreatMasterPrice=false;
           me.Form.changeDiscountGreatMasterPrice();
           me.Form.IsDiscountGreatMasterPrice=true;
            //找到三甲价格
	        var buttonsToolbar = me.Grid.getComponent('buttonsToolbar');
	    	var labMarketPrice = buttonsToolbar.getComponent('labMarketPrice');
            var MarketPrice = me.Form.getComponent('BLabTestItem_MarketPrice');
            var labMarketPriceVal=labMarketPrice.getValue();
            //设置三甲价格,并且不需要联动计算
            MarketPrice.IsMarketPrice=false;
            MarketPrice.setValue(labMarketPriceVal);
            MarketPrice.IsMarketPrice=true;
            //存在细项
           if(me.Grid.getStore().getCount()>0){
           	   var val=DiscountGreatMasterPrice.getValue()/100;
           	   me.Grid.changeGreatMasterPrice(val);
               me.Grid.changePrice();
           }
    	},null,500);
    },
    
    /**表单的项目价格改变
     *项目价格--项目价格折扣率--明细的执行价格 (三甲价格*项目价格折扣率)
	 *明细的执行价格 结束项，IsDiscountPrice=false,不再根据项目价格计算
	 * IsPrice,用于执行价格改变，
     * */
    formChangePrice:function(){
    	var me =this;
  		var Price = me.Form.getComponent('BLabTestItem_Price');
	    var DiscountPrice = me.Form.getComponent('BLabTestItem_DiscountPrice');
        me.Form.LinkPriceNoDiscountPrice();
        if(me.IsPrice){
           //存在细项
           if(me.Grid.getStore().getCount()>0){
           	   var val = DiscountPrice.getValue()/100;
           	   me.Grid.changeDiscountPrice(val);
               me.Grid.changePrice();
           }
        }
        me.IsPrice=true;
    },
    /**表单的项目价格折扣率改变
     *项目价格折扣率开始--执行价格改变(明细)-项目价格
	 * 项目价格 结束项  IsDiscountPrice=false 不根据折扣率再计算项目价格
	 * */
    formChangeDiscountPrice:function(){
    	var me =this;
        var Price = me.Form.getComponent('BLabTestItem_Price');
	    var DiscountPrice = me.Form.getComponent('BLabTestItem_DiscountPrice');
        me.Form.IsDiscountPrice=false;
        me.Form.changeDiscountPrice();
        me.Form.IsDiscountPrice=true;
       //存在细项
       if(me.Grid.getStore().getCount()>0){
       	   var val =DiscountPrice.getValue()/100;
       	   me.Grid.changeDiscountPrice(val);
           me.Grid.changePrice();
           var buttonsToolbar = me.Grid.getComponent('buttonsToolbar');
           var labPrice = buttonsToolbar.getComponent('labPrice');
           var v=labPrice.getValue();
           if(!v)v=0;
           me.Form.IsPriceLoad=false;
           Price.setValue(v);
           me.Form.IsPriceLoad=true;
       }
    },
     /**从表单的内部价格改变
	 *项目价格--内部价格折扣率--明细的内部价格 (三甲价格*内部价格折扣率)
	 *明细的内部价格 结束项，IsGreatMasterPriceLoad=false,不再根据内部价格计算
	 */
    formChangeMasterPrice:function(){
        var me =this;
        var Price = me.Form.getComponent('BLabTestItem_GreatMasterPrice');
	    var DiscountGreatMasterPrice = me.Form.getComponent('BLabTestItem_DiscountGreatMasterPrice');
	    me.Form.IsDiscountGreatMasterPrice=false;
	    me.Form.changeGreatMasterPrice();
	    me.Form.IsDiscountGreatMasterPrice=true;
	    if(me.IsGreatMasterPrice){
	   	 //存在细项
           if(me.Grid.getStore().getCount()>0){
           	   var val=DiscountGreatMasterPrice.getValue()/100;
           	   me.Grid.changeGreatMasterPrice(val);
               me.Grid.changePrice();
           }
	    }
        me.IsGreatMasterPrice=true;
    },
     /**内部价格折扣率开始--内部价格改变(明细)-内部价格
	 * 内部价格 结束项  IsDiscountPrice=false 不根据折扣率再计算项目价格
	 * */	
    formDiscountGreatMasterPrice:function(){
    	var me = this;
	    var Price = me.Form.getComponent('BLabTestItem_GreatMasterPrice');
	    var DiscountGreatMasterPrice = me.Form.getComponent('BLabTestItem_DiscountGreatMasterPrice');
        me.Form.IsDiscountGreatMasterPrice=false;
        me.Form.changeDiscountGreatMasterPrice();
        me.Form.IsDiscountGreatMasterPrice=true;
        //存在细项
        if(me.Grid.getStore().getCount()>0){
       	   var val=DiscountGreatMasterPrice.getValue()/100;
       	   me.Grid.changeGreatMasterPrice(val);
           me.Grid.changePrice();
           var buttonsToolbar = me.Grid.getComponent('buttonsToolbar');
           var labPrice = buttonsToolbar.getComponent('labGreatMasterPrice');
           var v=labPrice.getValue();
           if(!v)v=0;
           me.Form.IsDiscountGreatMasterPrice=false;
           Price.setValue(v);
           me.Form.IsDiscountGreatMasterPrice=true;
       }
    },
      /**内部价格折扣率开始--内部价格改变(明细)-内部价格
	 * 内部价格 结束项  IsDiscountPrice=false 不根据折扣率再计算项目价格
	 * */	
    formDiscountGreatMasterPrice2:function(){
    	var me = this;
	    var Price = me.Form.getComponent('BLabTestItem_GreatMasterPrice');
	    var DiscountGreatMasterPrice = me.Form.getComponent('BLabTestItem_DiscountGreatMasterPrice');
        me.Form.IsDiscountGreatMasterPrice=false;
        me.Form.changeDiscountGreatMasterPrice();
        me.Form.IsDiscountGreatMasterPrice=true;
        //存在细项
        if(me.Grid.getStore().getCount()>0){
       	   var val=DiscountGreatMasterPrice.getValue();
       	   me.Grid.changeGreatMasterPrice(val);
           me.Grid.changePrice();
           var buttonsToolbar = me.Grid.getComponent('buttonsToolbar');
           var labPrice = buttonsToolbar.getComponent('labGreatMasterPrice');
           var v=labPrice.getValue();
           if(!v)v=0;
           me.Form.IsDiscountGreatMasterPrice=false;
           Price.setValue(v);
           me.Form.IsDiscountGreatMasterPrice=true;
       }
    },
     /**
	 * 重新计算细项内部价格,表单的内部价格不用重新计算
	 * 细项内部价格没保存到数据库，因此需要重新计算
	 * */	
    CountGridGreatMasterPrice:function(){
    	var me = this;
	    var Price = me.Form.getComponent('BLabTestItem_GreatMasterPrice');
	    var DiscountGreatMasterPrice = me.Form.getComponent('BLabTestItem_DiscountGreatMasterPrice');
        me.Form.IsDiscountGreatMasterPrice=false;
        me.Form.changeDiscountGreatMasterPrice();
        me.Form.IsDiscountGreatMasterPrice=true;
        //存在细项
        if(me.Grid.getStore().getCount()>0){
       	   var val=DiscountGreatMasterPrice.getValue()/100;
       	   me.Grid.changeGreatMasterPrice(val);
           me.Grid.changePrice();
       }
    }
});