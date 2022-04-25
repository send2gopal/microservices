#stage 1
FROM node:latest as node
WORKDIR /home/app/
COPY ./microkart.frontend /home/app/
RUN ls  /home/app/
RUN npm install --force
RUN npm run build

#stage 2
FROM nginx:alpine
RUN rm -rf /usr/share/nginx/html/* && rm -rf /etc/nginx/nginx.conf
COPY --from=node /home/app/nginx.conf /etc/nginx/nginx.conf
COPY --from=node /home/app/default.conf /etc/nginx/conf.d/default.conf
COPY --from=node /home/app/dist/multikart/browser /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]