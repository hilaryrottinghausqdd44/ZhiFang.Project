/**
	@name：排序
	@author：liangyl	
	@version 2019-08-03
 */
layui.extend({
	uxutil: 'ux/util',
	uxtable:'ux/table'
}).define(['uxutil','uxtable','layer','table'],function(exports){
    "use strict";
    var $ = layui.$,
        uxutil = layui.uxutil,
        uxtable = layui.uxtable,
        table = layui.table,
        layer = layui.layer;
    
    var sortlist = {
    	config:{
    		page: false,
			limit: 1000,
			loading : true,	
			elem:'',
			title:'排序',
            url:'',	
            selectUrl:'',//查询列表url
            prefix:'',//前缀
            elemDiv:'',//DIV，id
            pkfield:'Id',//主键
			disporderfield:'DispOrder',//显示次序
			ordercols:[],//显示的列
		    editUrl:'',
		    defaultOrderBy:[],
            cols:[[
			   {type: 'checkbox',fixed: 'left'},
		 	   {field:'OldDispOrder', width:100,title: '原始值显示次序',sort: true,hide:true}
		    ]],
			text: {none: '暂无相关数据' },
			done: function(res, curr, count) {
				if(count>0){
					var filter = this.elem.attr("lay-filter");
			      	//默认选择第一行
					var rowIndex  = 0;
					var tableDiv = $("#"+filter+"+div .layui-table-body.layui-table-body.layui-table-main");
                    var thatrow = tableDiv.find('tr:eq(' + rowIndex + ')');
			        thatrow.click();
				}
			}
    	},
        //获取选中的tr
        checkrow_tr:'',
        //是否手动调整
        IsAdjustment:false
    };
   
    var Class = function(setings){
		var me = this;
		me.config = $.extend({
			parseData:function(res){//res即为原始返回的数据
				if(!res) return;
				var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
				var list = [];
				if(data && data.list){
					var disporderfield = me.config.disporderfield;
					for(var i=0;i<data.list.length;i++){
						data.list[i].OldDispOrder = data.list[i][me.config.disporderfield];
						list.push(data.list[i]);
					}
				}
				return {
					"code": res.success ? 0 : 1, //解析接口状态
					"msg": res.ErrorInfo, //解析提示文本
					"count": data.count || 0, //解析数据长度
					"data": list || []
				};
			},
			afterRender:function(that){
				var filter = $(that.config.elem).attr("lay-filter");
				if(filter){
					//监听行双击事件
					that.table.on('row(' + filter + ')', function(obj){
					    me.checkrow_tr =this;
						//标注选中样式
	                    obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
					});
				}
			}
		},me.config,sortlist.config,setings);
		me.createHTMLDocument();
	};
    
    Class.pt = Class.prototype;
    //初始化模板
    Class.pt.createHTMLDocument = function () {
        var me = this,
            elem  = me.config.elemDiv;
        //创建页面元素
        var html =
		    '<div class="layui-col-xs12">'+ 
		      '<div style="position: absolute;top: 0;right: 0;width: 230px;height: 100%;">'+  
		       '<div class="layui-card" style="height: 100%;"> '+ 
		        '<div class="layui-card-body"> '+ 
		         '<div class="layui-form"> '+ 
		          '<div class="layui-btn-container" style="text-align:center;"> '+ 
		           '<button itemid="btntopup" class="layui-btn layui-btn-xs"><i class="layui-icon layui-icon-next" style="display: inline-block;transform: rotate(-90deg);"></i>置顶</button></br> '+ 
		           '<button  itemid="btnup" class="layui-btn layui-btn-xs"><i class="layui-icon layui-icon-up"></i> 向上</button></br>'+  
		           '<button  itemid="btndown" class="layui-btn layui-btn-xs"><i class="layui-icon layui-icon-down"></i> 向下</button></br> '+ 
		           '<button itemid="btnbottomdown" class="layui-btn layui-btn-xs"><i class="layui-icon layui-icon-prev" style="display: inline-block;transform: rotate(-90deg);"></i>置底</button>'+  
		          '</div> '+ 
		          '<div class="layui-btn-group" style="text-align:center;">'+  
		           '<button  itemid="adjustment" class="layui-btn layui-btn-xs "><i class="layui-icon layui-icon-slider"></i>手工调整</button>'+  
		           '<button  itemid="adjustmentfinish" class="layui-btn layui-btn-xs "><i class="layui-icon layui-icon-util"></i>手动调整完成</button>'+  
		          '</div>'+  
	 	           '<div class="layui-form-item"> '+ 
		          ' <div style="text-align:center;padding-top:10px;">'+ 
		              ' 显示次序从'+ 
		               '<div class="layui-inline">'+ 
		                   '<input type="number" name="DispOrder" itemid="DispOrder" autocomplete="off" value="1" class="layui-input" style="width: 70px;" />'+ 
		               '</div>'+ 
		               '<div style="display: inline-block;width: 37px;">开始</div>'+ 
		           '</div>'+ 
		           '<div style="text-align:center;">'+ 
		               '间隔'+ 
		               '<div class="layui-inline">'+ 
		                   '<input type="number" name="interval" itemid="interval" autocomplete="off" value="1" class="layui-input" style="width: 70px;" />'+ 
		               '</div>'+ 
		           '</div>'+ 
		           '<div style="text-align:center;">'+ 
		             '<div class="layui-col-xs6">'+
		               '<button  itemid="btnreorder" class="layui-btn layui-btn-xs"><i class="layui-icon layui-icon-list"></i>选中重新排序</button>'+ 
		           	 '</div>'+   
		           	 '<div class="layui-col-xs6">'+
		           	    '<button type="button" itemid="btnclorder" class="layui-btn layui-btn-xs"><i class="layui-icon layui-icon-fonts-clear"></i>清除选中排序</button>'+
		             '</div>'+ 
		          '</div>'+
		          '</div> '+ 
		          '<div class="layui-form-item"> '+ 
		           '<div class="layui-btn-container" style="text-align:center;">'+  
		            '<button  itemid="save" class="layui-btn layui-btn-xs "><i class="iconfont">&#xe713;</i>&nbsp;保存</button></br>'+  
		            '<button  itemid="cancel" class="layui-btn layui-btn-xs layui-btn-primary"><i class="layui-icon">&#x1007;</i>取消</button> '+ 
		           '</div> '+ 
		          '</div> '+ 
		         '</div>'+  
		        '</div> '+ 
		       '</div> '+ 
		      '</div>'+  
		       '<div style="margin-right: 230px;height: 100%;padding-right: 5px;"> '+ 
		      	  '<table id="sortList" lay-filter="sortList"></table> '+ 
              '</div> '+ 
		     '</div>';
        $(elem).html(html)
    };
    //初始化表格
    Class.pt.render = function () {
        var me = this,options = me.config;
    };
    sortlist.render = function (options) {        
        var me = new Class(options);
	    var result = uxtable.render(me.config);
        me.config.cols = me.createcols();
		result.loadData = me.loadData;
		//加载数据
		result.loadData(me.config.where);
	    //监听
	    me.iniListeners();
		return result;
    };
    //监听
    Class.pt.iniListeners = function(){
    	var me = this;
        var view = $("body") ,
			btntopup = view.find("button[itemid='btntopup']"),//置顶
			btnup = view.find("button[itemid='btnup']"),//向上
			btndown = view.find("button[itemid='btndown']"),//向下
			btnbottomdown = view.find("button[itemid='btnbottomdown']"),//置底
			btnreorder = view.find("button[itemid='btnreorder']"),//重新排序
			btnclorder = view.find("button[itemid='btnclorder']"),//重新排序
			adjustment = view.find("button[itemid='adjustment']"),//手动调整
			adjustmentfinish = view.find("button[itemid='adjustmentfinish']"),//adjustmentfinish
            save = view.find("button[itemid='save']"),//保存
			cancel = view.find("button[itemid='cancel']");//取消
			
		//置顶	
		btntopup.on('click',function(){
			me.onUpMove(2);
		});
		//向上
		btnup.on('click',function(){
			me.onUpMove(1);
		});
		//向下
		btndown.on('click',function(){
			me.onDownMove(1);
		});
		//置顶
		btnbottomdown.on('click',function(){
			me.onDownMove(2);
		});
		//重新排序
		btnreorder.on('click',function(){
			me.reorder();
		});
		//清除
		btnclorder.on('click',function(){
			me.clorder();
		});
		//保存
		save.on('click',function(){
			me.onSaveClick();
		});
		//取消
		cancel.on('click',function(){
			var index = parent.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
	        parent.layer.close(index); //再执行关闭
		});
		 //手动调整
		adjustment.on('click',function(){
		  	me.config.IsAdjustment=true;
		  	me.isShowBtn(false);
		});
		  //手动调整完成
	    adjustmentfinish.on('click',function(obj){
		  	me.config.IsAdjustment=false;
		  	me.isShowBtn(true);
		});
		var filter = $(me.config.elem).attr("lay-filter");
		 //监听单元格事件
		table.on('tool('+filter+')', function(obj){
		    var data = obj.data;
		    if(obj.event === 'setDispOrder'){
		    	if(me.config.IsAdjustment){
		    		$(this).find('input').focus();
		    		$(obj.tr).find(".layui-table-edit").keyup(function () {
		                var $input = $(this), val = $input.val();
		                if (!val) val = "0";
		                $input.val(val.replace(/[^\d]/g, ''));
		            });
		    	}else{
		    		$(this).find('input').blur();
		    	}
		    }else{
		    	$(this).find('input').blur();
		    }
		});
	
    };
       //创建列
    Class.pt.createcols = function(){
        var me = this;
        //主键列和显示次序
        me.config.cols[0].push(
        	{field:me.config.pkfield,width: 150,title: '主键',sort: true,hide:true},
			{field:me.config.disporderfield,width: 100,title: '显示次序',sort: true, edit: 'text',event: "setDispOrder"});
		//其他列
        for(var i =0;i<me.config.ordercols.length;i++){
        	 me.config.cols[0].push(me.config.ordercols[i]);
        }
        return me.config.cols;
    };
    //列表数据加载
	Class.pt.loadData = function(whereObj){
		var me = this,
			cols = me.config.cols[0],
			fields = [];
		for(var i in cols){
			fields.push(cols[i].field);
		}
		me.instance.reload({
			url:me.config.selectUrl,
			where:$.extend({},whereObj,{
				fields:fields.join(',')
			})
		});
	};
	//默认选择行
    Class.pt.doAutoSelect = function(rowIndex){
    	var me = this;
    	var filter = $(me.config.elem).attr("lay-filter");
      	//默认选择第一行
		if(!rowIndex)rowIndex  = 0;
		var tableDiv = $(me.config.elem+"+div .layui-table-body.layui-table-body.layui-table-main");
        var thatrow = tableDiv.find('tr:eq(' + rowIndex + ')');
        thatrow.click();
    };
     /**向上移动方法  
     * type=1 向下移动一行
     * type=2 置底
    * */
    Class.pt.onUpMove = function(type){
    	var me = this;
    	//当前选择行
    	var tr = $(me.checkrow_tr);
    	var filter = $(me.config.elem).attr("lay-filter");
    	var tbData = table.cache[filter];
	    if (tr == null) {
	        layer.msg("请选择元素");
	        return;
	    }
	    if ($(tr).prev().html() == null) {
	        layer.msg("已经是最顶部了");
	        return;
	    }else{
	        //当前选择行数据
	        var tem = tbData[tr.index()];
	        var selectindex=0;
	        //向上
	        if(type==1){
	        	//当前选择上的上一行数据
		        var tem2 = tbData[tr.prev().index()];
		        // 将本身插入到目标tr之前
		        $(tr).insertBefore($(tr).prev());
		        var sss =me.config.prefix+"_DispOrder";
		         //获取本行显示次序
		        var DispOrder = tem[me.config.disporderfield];
		        //下一行显示次序
		        var nDispOrder = tem2[me.config.disporderfield];
	            if(!DispOrder)DispOrder=0;
	            if(!nDispOrder)nDispOrder=0;
	             //本行显示次序-1
	            tem[me.config.disporderfield] = Number(DispOrder)-1;
	            //上一行显示次序+1
	            tem2[me.config.disporderfield] = Number(nDispOrder)+1;
		        //上移之后，数据交换
		        tbData[tr.index()] = tem;
		        tbData[tr.next().index()] = tem2;
		        selectindex=tr.index();
	        }else{
	        	//删除当前选择行数据
	        	tbData.splice(tr.index(),1);
		        //获取第一行的显示次序
		        var DispOrder = tbData[0][me.config.disporderfield];
		        tem[me.config.disporderfield] = Number(DispOrder)-1;
	            tbData.splice(0,0,tem);
	            selectindex = 0;
	        }
	        table.reload(filter, {
	        	url:'',
	            data: tbData
	        });
	        me.doAutoSelect(selectindex);
	    }
    };
     /**向下移动方法  
     * type=1 向下移动一行
     * type=2 置底
     * */
    Class.pt.onDownMove = function(type){
    	var me = this;
    	//当前选择行
    	var tr = $(me.checkrow_tr);
    	var filter = $(me.config.elem).attr("lay-filter");
    	var tbData = table.cache[filter];
	    if (tr == null) {
	        layer.msg("请选择行");
	        return;
	    }
	    if ($(tr).next().html() == null) {
	        layer.msg("已经是最底部了");
	        return;
	    } else{
	        //选择行数据本行
	        var tem = tbData[tr.index()];
	        if(type==1){
	        	 //选择行的下一行数据
	            var tem2 = tbData[tr.next().index()];
	        	//获取本行显示次序
		        var DispOrder = tem[me.config.disporderfield];
		        //下一行显示次序
		        var nDispOrder = tem2[me.config.disporderfield];
	            if(!DispOrder)DispOrder=0;
	            if(!nDispOrder)nDispOrder=0;
	            //本行显示次序+1
	            tem[me.config.disporderfield] = Number(DispOrder)+1;
	            //上一行显示次序-1
	            tem2[me.config.disporderfield] = Number(nDispOrder)-1;
		        // 将本身插入到目标tr的后面
		        $(tr).insertAfter($(tr).next());
		        // 交换数据
		        tbData[tr.index()] = tem;
		        tbData[tr.prev().index()] = tem2;
		        var selectindex= tr.index();
	        }else{
	        	//删除当前选择行数据
	        	tbData.splice(tr.index(),1);
		        //获取最后一行索引
		        var lasslen = tbData.length-1;
		        //获取最后一行数据
		        var tem2 = tbData[lasslen];
		        //获取最后一行的显示次序
		        var DispOrder = tem2[me.config.disporderfield];
		        tem[me.config.disporderfield] = Number(DispOrder)+1;
	            tbData.push(tem);
	            if(tbData.length==0)tbData.length=1;
	            var selectindex = tbData.length-1;
	        }
	        table.reload(filter, {
	        	url:'',
	            data: tbData
	        });
	        me.doAutoSelect(selectindex);
	    }
    };
      //重新排序
    Class.pt.reorder = function(){
    	var me = this;
    	
    	//从什么开始
    	var DispOrder = $("input[name='DispOrder']").val();  
    	if(!DispOrder){
    		layer.msg("请输入显示次序！", { icon: 5, anim: 6 });
    		return;
        }
    	var interval = $("input[name='interval']").val();  
        if (!interval) {
            layer.msg("请输入显示次序间隔！", { icon: 5, anim: 6 });
            return;
        }
    	var filter = $(me.config.elem).attr("lay-filter");
    	var tbData = table.cache[filter];
    	var checkStatus = table.checkStatus(filter),
            checkDatas= checkStatus.data;
        if(checkDatas.length==0){
    		layer.msg("请勾选要重新排序是的数据!", { icon: 5, anim: 6 });
    		return;
    	}
        for (var i = 0; i < tbData.length; i++){
    		var LBSectionItemId = tbData[i][me.config.pkfield];
    		for(var j=0;j<checkDatas.length;j++){
    			if(LBSectionItemId ==checkDatas[j][me.config.pkfield] ){
                    DispOrder = Number(DispOrder);
                    if (j > 0) DispOrder = DispOrder + Number(interval);
		    	    tbData[i][me.config.disporderfield] =JSON.stringify(DispOrder);
    				break;
    			}
    		}
    	}
        table.reload(filter, {
        	url:'',
            data: tbData
        });
        me.doAutoSelect();
    };
        //清除所有排序
    Class.pt.clorder = function () {
    	var me = this;
	    var filter = $(me.config.elem).attr("lay-filter");
    	var tbData = table.cache[filter];
        var checkStatus = table.checkStatus(filter),
            checkDatas = checkStatus.data;
        if (checkDatas.length == 0) {
            layer.msg("请选中再进行清除排序！");
            return;
        }
        for (var i = 0; i < tbData.length; i++) {
            var Id = tbData[i][me.config.pkfield];
            for (var j = 0; j < checkDatas.length; j++) {
                if (Id == checkDatas[j][me.config.pkfield]) {
                    var DispOrder = 0;
                    tbData[i][me.config.disporderfield] = JSON.stringify(DispOrder);
                    break;
                }
            }
        }
        table.reload(filter, {
            url: '',
            data: tbData
        });
        me.doAutoSelect();
    };
    //保存方法
	Class.pt.onSaveClick =  function(){
		var me = this;
		var records = me.getModifiedRecords();
        if(records.length==0){
			layer.msg('没有修改的数据不需要保存！');
            return;
		}		
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = records.length;
		//显示遮罩
		if(records.length==0)return;
		var indexs=layer.load();
		//获取列表数据
	    for(var i = 0;i<records.length;i++){
	    	//找到修改过数据的行
	    	me.updateOne(i,records[i]);
	    }
	};
	//获取修改过的行记录
    Class.pt.getModifiedRecords =  function(){
    	var me = this,list=[];
    	var filter = $(me.config.elem).attr("lay-filter");
    	//获取列表数据
		var tableCache = table.cache[filter];
	    for(var i = 0;i<tableCache.length;i++){
	    	//找到修改过数据的行
	    	if(tableCache[i].OldDispOrder != tableCache[i][me.config.disporderfield]){
	    		list.push(tableCache[i]);
	    	}
	    }
	    return list;
    };
    //按钮可用
    Class.pt.isShowBtn = function(bo){
    	  var view = $("body") ,
			btntopup = view.find("button[itemid='btntopup']"),//置顶
			btnup = view.find("button[itemid='btnup']"),//向上
			btndown = view.find("button[itemid='btndown']"),//向下
			btnbottomdown = view.find("button[itemid='btnbottomdown']");
    	if(bo){
    		$(btntopup).removeClass('layui-btn-disabled').removeAttr('disabled',"true");
    		$(btnup).removeClass('layui-btn-disabled').removeAttr('disabled',"true");
    		$(btndown).removeClass('layui-btn-disabled').removeAttr('disabled',"true");
    		$(btnbottomdown).removeClass('layui-btn-disabled').removeAttr('disabled',"true");
    	}else{
    	    $(btntopup).prop("disabled", "disabled");
            $(btntopup).addClass('layui-btn-disabled');
            
            $(btnup).prop("disabled", "disabled");
            $(btnup).addClass('layui-btn-disabled');
            
            $(btndown).prop("disabled", "disabled");
            $(btndown).addClass('layui-btn-disabled');
            
            $(btnbottomdown).prop("disabled", "disabled");
            $(btnbottomdown).addClass('layui-btn-disabled');
    	}
    };
    Class.pt.updateOne =  function(index,obj){
   		var me = this;
   		setTimeout(function() {
   	        var  id = obj[me.config.pkfield];
   	        var  DispOrder = obj[me.config.disporderfield];
            var entity ={
            	Id:id,
            	DispOrder:DispOrder
            };
            var fields ="Id,DispOrder";
            var params={entity:entity,fields:fields};
		    params = JSON.stringify(params);
           //显示遮罩层
			var config = {
				type: "POST",
				url: me.config.editUrl,
				data: params
			};
   			uxutil.server.ajax(config, function(data) {
				if (data.success) {
					me.saveCount++;
				} else {
					me.saveErrorCount++;
				}	
				if (me.saveCount + me.saveErrorCount == me.saveLength) {
					if (me.saveErrorCount == 0){
						layer.msg('保存成功', {
							time:200
						}, function(){
							parent.layer.closeAll('iframe');
							parent.afterSortUpdate(data);
						});
					}else{
						layer.msg(data.msg,{ icon: 5, anim: 6 });
					}
				}
			})
		}, 100 * index);
   	};
    exports('sortlist',sortlist);
})