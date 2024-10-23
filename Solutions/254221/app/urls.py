from django.contrib import admin
from django.urls import path

from app.views import ProjectListView

urlpatterns = [
    path('admin/', admin.site.urls),
    path('projects/', ProjectListView.as_view(), name='projects'),
]
