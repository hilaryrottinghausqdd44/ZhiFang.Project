Ext.define("ZhiFang.store.FormViewStore",{
	extend:'Ext.data.Store',
	fields : ['L1','L2','L3'], 
	pageSize:2,
	remoteSort: true,
	proxy: {
        type: 'pagingmemory',
        data : [
	    	[11,12,13],
	    	[21,22,23],
	    	[31,32,33],
	    	[41,42,43],
	    	[51,52,53],
	    	[61,62,63],
	    	[71,72,73]
		],
        reader: {
            type: 'array'
        }
    },
    autoLoad:true
});