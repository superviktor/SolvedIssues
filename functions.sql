--hello world 
create function dbo.helloworld()
returns varchar(20)
as 
begin
return 'hello world'
end

--exec
select dbo.helloworld()

--params
create function dbo.f_celsiustofahrenheit(@celcius real)
returns real
as 
begin
return @celcius*1.8+32
end

--exec 
select dbo.f_celsiustofahrenheit(0) as fh

--reusability
select concat(dbo.helloworld(),' you')