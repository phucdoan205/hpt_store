using AutoMapper;
using backend.DTOs.Products;
using backend.Models.Products;
using backend.DTOs.Catalog.Product;
using backend.DTOs.Catalog.Category;
using backend.DTOs.Shopping.Order;
using backend.Models.Orders;

using backend.DTOs.Users;
using backend.Models.Users;

using backend.DTOs.Auth;
using backend.DTOs.Admin;

namespace backend.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductResponseDto>();
            CreateMap<CreateProductRequestDto, Product>();
            CreateMap<UpdateProductRequestDto, Product>();

            CreateMap<ProductVariant, ProductVariantDto>();
            CreateMap<ProductImage, ProductImageDto>();

            CreateMap<Category, CategoryResponseDto>();
            CreateMap<CreateCategoryRequestDto, Category>();

            CreateMap<Order, OrderResponseDto>();
            CreateMap<CreateOrderRequestDto, Order>();

            CreateMap<OrderDetail, OrderDetailDto>();

            CreateMap<User, UserResponseDto>();
            CreateMap<User, UserProfileDto>();
            CreateMap<CreateUserRequestDto, User>();
            CreateMap<UpdateUserRequestDto, User>();
            CreateMap<CreateStaffRequestDto, User>();
        }
    }
}