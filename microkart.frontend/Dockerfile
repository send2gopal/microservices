#stage 1
FROM node:latest as node
WORKDIR /home/app/
COPY ./microkart.frontend /home/app/

RUN ls
RUN npm install --force
RUN npm run build:ssr
#stage 2
FROM nginx:alpine
COPY --from=node /home/app/dist/multikart/browser /usr/share/nginx/html
EXPOSE 80