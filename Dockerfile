#Use the official Ubuntu base image 
FROM ubuntu:20.04  
#Install NGINX 
RUN apt-get update \
&& apt-get install -y sudo \
&& apt-get install -y nginx
#Install git
RUN apt install git -y

#Create directory for F5_Webapp  
RUN mkdir /var/www/F5_Demo_Web_App/ \ 
&& cd /var/www/F5_Demo_Web_App/ \ 
&& git init \ 
&& git clone https://github.com/baex3/F5_Demo_Web_App.git

#Create directory for basic sample apps
RUN mkdir /var/www/basic_sample_apps/   

# Copy a custom Nginx configuration file (optional) 
COPY nginx.conf /etc/nginx/ 

#Copy sample apps into serving directories
COPY F5_Demo_Web_App/  /var/www/F5_Demo_Web_App/
COPY basic_sample_apps/  /var/www/basic_sample_apps/
VOLUME [ /var/www/ ]  
# Expose port 80 to allow incoming HTTP traffic 
EXPOSE 80 5000 8001 8002 8003 8004 
# Start Nginx when the container launches
CMD ["/usr/sbin/nginx", "-g", "daemon off;"]
#CMD tail -f /dev/null 
