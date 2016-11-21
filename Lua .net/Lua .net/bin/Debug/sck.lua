 function sck()
print("Socket activated !!!")
--socket.tcp()
s = assert( socket.bind("127.0.0.1",8080))
c= assert(s:accept())
a,b = c:receive()
 while not b do
 l,e = c:receive() 
  print(l.."\n\0")
  -- print(e)
  -- print(a)
  -- print(b)
end
end
 
 