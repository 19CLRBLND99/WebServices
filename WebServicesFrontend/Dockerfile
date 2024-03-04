#Use node to install dependencies and build the app in "production mode"
FROM node:latest as build
WORKDIR /src/app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build -- --configuration=production

# Use an nginx:alpine server as the base for the app that was built above
FROM nginx:alpine
COPY --from=build /src/app/dist/web-services-frontend/browser /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]