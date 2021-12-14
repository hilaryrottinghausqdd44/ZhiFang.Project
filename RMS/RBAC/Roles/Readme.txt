select a.id,a.name,b.bid from tmp1 a  inner join
(
  select distinct aid,bid=dbo.get_str(aid) from tmp2
) b on a.id=b.aid

create function get_str(@aid int)   
  returns varchar(8000)   
  as     
  begin   
          declare @ret varchar(8000)   
          set @ret = ''   
            
          select @ret = @ret + ',' + cast(bid as varchar) from tmp2 where aid=@aid 
            
          set @ret = stuff(@ret,1,1,'')   
          return @ret   
  end   